using System;
using EPiServer;
using EPiServer.Core;
using Machine.Specifications;
using WhiteMagic.Tests;

namespace EPi7Test.Examples.Tests
{
    public class TestContext
    {
        protected static IContentRepository ContentRepository;

        Establish context = () =>
            {
                ContentRepository = new InMemoryContentRepository(StandardActivator, new InMemoryPermanentLinkMapper());
                ContentRepository.CreateSystemPages();
            };

        private static IContentData StandardActivator(Type type)
        {
            return Activator.CreateInstance(type) as IContentData;
        }
    }
}
