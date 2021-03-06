using System.Linq;
using System.Threading.Tasks;
using stiebel_eltron_apiserver.Core.Entities;
using stiebel_eltron_apiserver.UnitTests;
using Xunit;

namespace stiebel_eltron_apiserver.IntegrationTests.Data
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task AddsItemAndSetsId()
        {
            var repository = GetRepository();
            var item = new ToDoItemBuilder().Build();

            await repository.AddAsync(item);

            var newItem = (await repository.ListAsync<ToDoItem>())
                            .FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }
    }
}
