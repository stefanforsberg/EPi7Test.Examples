using EPi7Test.Examples.Models.Pages;
using EPiServer.Core;
using Machine.Specifications;
using WhiteMagic.Tests;

namespace EPi7Test.Examples.Tests.TravelTips
{
    [Subject("TravelTips")]
    public class When_a_hotel_has_related_tips : TestContext
    {
        private static HotelPage _hotelPage;
        private static ContentReference _hotelPageRef;

        Establish context = () =>
            {
                _hotelPageRef = ContentRepository.Publish<HotelPage>(ContentReference.RootPage, p =>
                    {
                        p.PageName = "Hotel California";
                    });

                ContentRepository.Publish<TravelTipsPage>(ContentReference.RootPage, p =>
                    {
                        p.PageName = "Tips 1";
                        p.RelatedHotelPage = _hotelPageRef;
                    });

                ContentRepository.Publish<TravelTipsPage>(ContentReference.RootPage, p =>
                    {
                        p.PageName = "Tips 2";
                        p.RelatedHotelPage = _hotelPageRef;
                    });
            };

        Because of = () => _hotelPage = ContentRepository.Get<HotelPage>(_hotelPageRef);

        It should_say_that_tips_exists_for_page = () => _hotelPage.HasTips.ShouldBeTrue();

        It should_return_tips_related_to_hotel = () =>
            {
                _hotelPage.TipsList[0].PageName.ShouldEqual("Tips 1");
                _hotelPage.TipsList[1].PageName.ShouldEqual("Tips 2");
            };
    }
}
