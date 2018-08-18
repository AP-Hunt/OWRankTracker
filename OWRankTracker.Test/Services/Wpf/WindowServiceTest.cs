using Autofac;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Services.Wpf
{
    [TestClass]
    public class WindowServiceTest
    {
        [TestMethod]
        public void UsesTheInjectedContainer_ToGetANewInstanceOfTheWindowType()
        {
            // Arrange
            FakeWindow window = new FakeWindow();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(window);
            IContainer container = builder.Build();

            OWRankTracker.Services.Wpf.WindowService service = new OWRankTracker.Services.Wpf.WindowService(container);

            // Act
            FakeWindow actual = service.ShowWindow<FakeWindow>();

            // Assert
            Assert.AreSame(window, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenInstanceCannotBeResolved_ThrowsException()
        {
            // Arrange
            ContainerBuilder builder = new ContainerBuilder();
            IContainer container = builder.Build();

            OWRankTracker.Services.Wpf.WindowService service = new OWRankTracker.Services.Wpf.WindowService(container);

            // Act
            service.ShowWindow<FakeWindow>();
        }

        [TestMethod]
        public void ShowsTheWindowBeforeReturningIt()
        {
            // Arrange
            FakeWindow window = new FakeWindow();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(window);
            IContainer container = builder.Build();

            OWRankTracker.Services.Wpf.WindowService service = new OWRankTracker.Services.Wpf.WindowService(container);

            // Act
            FakeWindow actual = service.ShowWindow<FakeWindow>();

            // Assert
            Assert.IsTrue(actual.IsVisible);
        }
    }
}
