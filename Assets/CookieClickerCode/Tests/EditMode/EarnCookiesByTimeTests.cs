using System;
using CookieClickerCode.Runtime.Domain;
using CookieClickerCode.Runtime.Presenter;
using NUnit.Framework;

namespace CookieClickerCode.Tests.EditMode
{
    public class EarnCookiesByTimeTests
    {
        private static void CreateSUT(out EarnCookiesByTime sut, out CookieClicker cookieClicker)
        {
            cookieClicker = CookieClicker.CreateEmpty();
            var outputCounter = new MockOutputCounter();
            var earnCookiePresenter = new EarnCookie(cookieClicker, outputCounter);
            sut = new EarnCookiesByTime(earnCookiePresenter, new Autoclicker(cookieClicker));
        }
        
        [Test]
        public void NotEarnsCookies()
        {
            CreateSUT(out var sut, out var cookieClicker);

            sut.Execute(new DateTime());

            Assert.Fail();

            Assert.AreEqual(0, cookieClicker.Cookies);
        }

        [Test]
        public void EarnACookieInASecond()
        {
            CreateSUT(out var sut, out var cookieClicker);
            cookieClicker.ClicksPerSecond = 1;
            
            sut.Execute(new DateTime());
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(1));
            
            Assert.AreEqual(1, cookieClicker.Cookies);
        }

        [Test]
        public void AccumulateTime()
        {
            CreateSUT(out var sut, out var cookieClicker);
            cookieClicker.ClicksPerSecond = 1;
                
            sut.Execute(new DateTime());
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(1));
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(1.5));
                
            Assert.AreEqual(1, cookieClicker.Cookies);
        }

        [Test]
        public void dfagasdf()
        {
            CreateSUT(out var sut, out var cookieClicker);
            
            cookieClicker.ClicksPerSecond = 1;
            sut.Execute(new DateTime());
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(0.5));
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(1));
            
            Assert.AreEqual(1, cookieClicker.Cookies);
        }
        [Test]
        public void TwoClicksPerSecond()
        {
            CreateSUT(out var sut, out var cookieClicker);

            cookieClicker.ClicksPerSecond = 2;
            sut.Execute(new DateTime());
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(0.5f));
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(1.1f));
            
            Assert.AreEqual(2, cookieClicker.Cookies);
        }

        [Test]
        public void dfasdfa()
        {
            CreateSUT(out var sut, out var cookieClicker);

            cookieClicker.ClicksPerSecond = 0;
            sut.Execute(new DateTime());
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(10));
            
            cookieClicker.ClicksPerSecond = 1;
            sut.Execute(new DateTime() + TimeSpan.FromSeconds(10.1));
            
            Assert.AreEqual(0, cookieClicker.Cookies);
        }
    }
}