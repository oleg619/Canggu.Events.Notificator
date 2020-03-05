﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Asp.Mediator.Base;
using CangguEvents.Asp.Models;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Models.Responses;
using CangguEvents.Asp.Services;
using CangguEvents.Asp.Services.Base;
using Shared;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Asp.Mediator.Handlers
{
    public class EventsHandler : IRequestHandlerDomain<EventsCommand>, IRequestHandlerDomain<FullEventInfoCommand>,
        IRequestHandlerDomain<ShortEventInfoCommand>
    {
        private readonly IEventsRepository _repository;
        private readonly IUserStateRepository _stateRepository;

        public EventsHandler(
            IEventsRepository repository,
            IUserStateRepository stateRepository)
        {
            _repository = repository;
            _stateRepository = stateRepository;
        }

        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(EventsCommand command,
            CancellationToken cancellationToken)
        {
            var eventInfos = await GetEventInfos(command);
            var userState = await _stateRepository.GetUserState(command.UserId, cancellationToken);
            return eventInfos.SelectMany(info => GetResponseForEvents(info, userState)).ToList();
        }

        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(FullEventInfoCommand request,
            CancellationToken cancellationToken)
        {
            var eventInfo = await _repository.GetEvent(request.EventId, cancellationToken);
            var inlineKeyboardButton =
                InlineKeyboardButton.WithCallbackData("Hide", $"{CommandMessages.CallbackHide}:{eventInfo.Id}");

            return new List<ITelegramResponse>
            {
                new EditKeyboardTelegramResponse(new InlineKeyboardMarkup(inlineKeyboardButton),
                    FormatCaption(eventInfo))
            };
        }

        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(ShortEventInfoCommand request,
            CancellationToken cancellationToken)
        {
            var eventInfo = await _repository.GetEvent(request.EventId, cancellationToken);

            var inlineKeyboardButton =
                InlineKeyboardButton.WithCallbackData("Show more", $"{CommandMessages.CallbackHide}:{eventInfo.Id}");

            return new List<ITelegramResponse>
            {
                new EditKeyboardTelegramResponse(new InlineKeyboardMarkup(inlineKeyboardButton), eventInfo.Name)
            };
        }

        private static IEnumerable<ITelegramResponse> GetResponseForEvents(EventInfo eventInfo, UserState userState)
        {
            if (userState.ShortInfo)
            {
                return ShortInfoEventResult(eventInfo);
            }

            var caption = FormatCaption(eventInfo);

            var telegramResponsePhoto = new PhotoTelegramResponse(eventInfo.Image, caption);
            var telegramResponseLocation = new LocationTelegramResponse(eventInfo.Location);

            return new List<ITelegramResponse> {telegramResponsePhoto, telegramResponseLocation};
        }

        private static IEnumerable<ITelegramResponse> ShortInfoEventResult(EventInfo eventInfo)
        {
            var inlineKeyboardMarkup =
                new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Show more", $"full:{eventInfo.Id}"));
            return new[] {new KeyboardTelegramResponse(inlineKeyboardMarkup, eventInfo.Name)};
        }

        private static string FormatCaption(EventInfo eventInfo)
        {
            var works = eventInfo.DayOfWeeks.Count switch
            {
                7 => "Works whole week",
                2 when eventInfo.DayOfWeeks.Contains(DayOfWeek.Sunday) &&
                       eventInfo.DayOfWeeks.Contains(DayOfWeek.Saturday) => "Works at weekend",
                _ => $"Works on : {string.Join(" | ", eventInfo.DayOfWeeks.Select(ShortDayNames.Get))}"
            };

            return $"[{eventInfo.Name}]({eventInfo.Location.GoogleUrl})\n{eventInfo.Description}\n*{works}*";
        }

        private Task<List<EventInfo>> GetEventInfos(EventsCommand command)
        {
            return command.OneDay ? _repository.GetEvents(command.DayOfWeek) : _repository.GetAllEvents();
        }
    }
}