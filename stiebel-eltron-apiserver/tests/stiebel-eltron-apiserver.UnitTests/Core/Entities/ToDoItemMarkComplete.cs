using System.Linq;
using stiebel_eltron_apiserver.Core.Events;
using Xunit;

namespace stiebel_eltron_apiserver.UnitTests.Core.Entities
{
    public class ToDoItemMarkComplete
    {
        [Fact]
        public void SetsIsDoneToTrue()
        {
            var item = new ToDoItemBuilder()
                .WithDefaultValues()
                .Description("")
                .Build();

            item.MarkComplete();

            Assert.True(item.IsDone);
        }

        [Fact]
        public void RaisesToDoItemCompletedEvent()
        {
            var item = new ToDoItemBuilder().Build();

            item.MarkComplete();

            Assert.Single(item.Events);
            Assert.IsType<ToDoItemCompletedEvent>(item.Events.First());
        }
    }
}
