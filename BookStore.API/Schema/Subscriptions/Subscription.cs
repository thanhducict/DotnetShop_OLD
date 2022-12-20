using BookStore.API.Schema.Mutations;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace BookStore.API.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public BookResult BookCreated([EventMessage] BookResult bookResult) => bookResult;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<BookResult>> BookUpdated(int bookId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{bookId}_{nameof(Subscription.BookUpdated)}";

            return topicEventReceiver.SubscribeAsync<string, BookResult>(topicName);
        }
    }
}
