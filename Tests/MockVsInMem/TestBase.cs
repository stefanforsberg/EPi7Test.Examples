using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPi7Test.Examples.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using WhiteMagic.Tests;

namespace EPi7Test.Examples.Tests.MockVsInMem
{
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
            Given();
            When();
        }

        public virtual void Given() {}

        public virtual void When() { }
    }

    public class When_using_mocks : TestBase
    {
        private IContentRepository _contentRepository;
        private HotelPage _hotelPage;
        public override void Given()
        {
            base.Given();

            _contentRepository = A.Fake<IContentRepository>();

            _hotelPage = new HotelPage {HasTips = true};

            A.CallTo(() => _contentRepository.Get<HotelPage>(A<ContentReference>.That.Matches(c => c.ID == 3)))
                .Returns(_hotelPage);
        }

        [Test]
        public void It_can_easily_behave_differently_from_the_real_content_repository()
        {
            _contentRepository.Get<HotelPage>(new ContentReference(3)).HasTips.ShouldBe(true);

            _hotelPage.HasTips = false;

            _contentRepository.Get<HotelPage>(new ContentReference(3)).HasTips.ShouldBe(true);
        }
    }

    public class When_using_in_memory : TestBase
    {
        private IContentRepository _contentRepository;
        private HotelPage _hotelPage;
        private ContentReference _hotelPageRef;

        public override void Given()
        {
            base.Given();

            _contentRepository = new InMemoryContentRepository(type => Activator.CreateInstance(type) as IContentData, new InMemoryPermanentLinkMapper());

            _hotelPage = new HotelPage { HasTips = true };

            _hotelPage = _contentRepository.GetDefault<HotelPage>(ContentReference.RootPage);
            _hotelPage.HasTips = true;
            _hotelPageRef = _contentRepository.Save(_hotelPage, SaveAction.Publish, AccessLevel.NoAccess);
        }

        [Test]
        public void It_can_easily_behave_differently_from_the_real_content_repository()
        {
            _contentRepository.Get<HotelPage>(_hotelPageRef).HasTips.ShouldBe(true);

            _hotelPage.HasTips = false;

            _contentRepository.Get<HotelPage>(_hotelPageRef).HasTips.ShouldBe(true);
        }
    }
}
