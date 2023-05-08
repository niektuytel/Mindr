using Mindr.Core.Interfaces;
using Mindr.Core.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.Core.Tests
{
    public class CalendarControllerTests
    {
        private Mock<ICalendarEventsProvider> calendarEventsProviderMock;
        private CalendarController calendarController;

        [SetUp]
        public void Setup() {
            calendarEventsProviderMock = new Mock<ICalendarEventsProvider>();
            calendarController = new CalendarController(calendarEventsProviderMock.Object);
        }

        [TestCase]
        public async System.Threading.Tasks.Task ShallReturnCalendarEventsFromProviderAsync() {
            
            calendarEventsProviderMock.Setup(p => p.GetEventsInMonthAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>  {
                
                var cal = new AgendaEvent()
                {
                    Subject = "MySubject",
                    StartDate = new DateTime(2020, 6, 1),
                    EndDate = new DateTime(2020, 6, 1)
                };

                var result = new ConcurrentBag<AgendaEvent>();
                result.Add(cal);
                return Task.FromResult<ConcurrentBag<AgendaEvent>>(result);

            });
            var result = await calendarController.GetEventsInMonthAsync(2020, 6);
            calendarEventsProviderMock.Verify(p => p.GetEventsInMonthAsync(2020, 6), Times.Once);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().EndDate, Is.EqualTo(new DateTime(2020, 6, 1)));
        }

        [TestCase]
        public async Task ShallAddCalendarEvents() {
            await calendarController.AddEventAsync(new AgendaEvent { Subject = "Mock"});
            calendarEventsProviderMock.Verify(p => p.AddEventAsync(It.IsAny<AgendaEvent>()), Times.Once);
        }

        [TestCase]
        public void ShallBuildCalendarDays() {
            List<CalendarDay> calendarDays = calendarController.BuildMonthCalendarDays(2020, 6);
            Assert.That(calendarDays.Count(), Is.EqualTo(CalendarController.COUNT_DAYS_IN_CALENDAR)); //refactor to use the const
            Assert.That(calendarDays.First().DayNumber, Is.EqualTo(31));
            Assert.That(calendarDays.First().IsEmpty);
            Assert.That(calendarDays.ElementAt(30).IsEmpty, Is.EqualTo(false));
            Assert.That(calendarDays.ElementAt(30).DayNumber, Is.EqualTo(30));
            Assert.That(calendarDays.ElementAt(31).DayNumber, Is.EqualTo(1));
            Assert.That(calendarDays.ElementAt(31).IsEmpty);

        }
    }
}
