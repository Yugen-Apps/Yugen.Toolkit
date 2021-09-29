using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;
using Yugen.Toolkit.Uwp.CodeChallenge.Model;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Services
{
    public class DummyApiService: IDummyApiService
    {
        public async Task<IEnumerable<ValueModel>> GetValueModelsAsync()
        {
            await Task.Delay((int) (5000 * new Random().NextDouble()));
            return new List<ValueModel>
            {
                new ValueModel
                {
                    Title = "Service-oriented",
                    Description = "Service-oriented means we plan ahead to ensure we can tackle problems and capitalise on opportunities at an early stage. We work hand in hand with each other and contribute proactively to the success of projects. We work in a precise manner and meet our deadlines.",
                    Order = 1,
                    Claim = "Service-oriented over efficient: Better results are achieved by working as a team rather than maximising the own efficiently by executing all tasks isolated."
                },

                new ValueModel
                {
                    Title = "Trustworthy",
                    Description = "Trustworthy means that you can rely on each and every one of us. We are consistent in our approach. We keep our promises, communicate proactively, do not withhold information and are always committed to the task at hand. We are also strong enough to admit our weaknesses to ultimately earn trust.",
                    Order = 4,
                    Claim = null
                },

                new ValueModel
                {
                    Title = "Curious",
                    Description = "Curious means we never cease to learn and are always open to new developments. This also means we are not afraid of making mistakes. We ask the right questions to deepen our understanding. We are prepared to take risks, adopt new ways of thinking and incorporate new approaches.",
                    Order = 2,
                    Claim = null
                },

                new ValueModel
                {
                    Title = "Result-driven",
                    Description = "Result-driven means we measure our performance on the basis of our results, which we are committed to achieving. We strive relentlessly to make progress and have high-quality standards. We demonstrate a great sense of responsibility to achieve our objectives.",
                    Order = 3,
                    Claim = "Result-driven over deliberation: Better results are achieved by getting things done rather than deliberating endlessly."
                },

                new ValueModel
                {
                    Title = "Appreciative",
                    Description = "Appreciative means we value respect and listen to each other. We praise each other for good work and share success. But being appreciative also means challenging one another and interacting in an open, honest and constructive manner.",
                    Order = 0,
                    Claim = "Appreciative over direct: Better results are achieved by being thoughtfully respectful than by being concisely direct."
                }
            };
        }
    }
}
