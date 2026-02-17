using FluentAssertions;
using SolarMonitor.Domain.Entities;
using SolarMonitor.Domain.Enums;
using Xunit;

namespace SolarMonitor.Tests
{
    public class PanelTests
    {
        [Fact]
        public void Validate_ShouldReturnError_WhenBrandIsEmpty()
        {
            Action act = () => new Panel("", "Model X", PanelType.Monocrystalline);
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Brand cannot be empty*");
        }

        [Fact]
        public void Validate_ShouldPass_WhenBrandIsValid()
        {
            var panel = new Panel("Tesla", "Model X", PanelType.Monocrystalline);
            panel.Brand.Should().Be("Tesla");
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenModelIsEmpty()
        {
            Action act = () => new Panel("Tesla", "", PanelType.Monocrystalline);
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Model cannot be empty*");
        }

        [Fact]
        public void Validate_ShouldPass_WhenModelIsValid()
        {
            var panel = new Panel("Tesla", "Model X", PanelType.Monocrystalline);
            panel.Model.Should().Be("Model X");
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(-10)]
        public void Validate_ShouldReturnError_WhenWattsAreNegative(double Watts)
        {
            var Voltage = 220;
            var panel = new Panel("Tesla", "Model X", PanelType.Monocrystalline);

            Action act = () => panel.RecordReading(Watts, Voltage);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*negative watts*"); 
        }

        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(60)]
        public void Validate_ShouldPass_WhenWattsIsValid(double Watts)
        {
            var Voltage = 220;
            var panel = new Panel("Tesla", "Model X", PanelType.Monocrystalline);

            Action act = () => panel.RecordReading(Watts, Voltage);
            act(); 
            panel.Readings.Should().ContainSingle(r => r.Watts == Watts && r.Voltage == Voltage);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-50)]
        [InlineData(-220)]
        public void Validate_ShouldReturnError_WhenVoltageAreNegative(double Voltage)
        {
            var Watts = 60;
            var panel = new Panel("Tesla", "Model X", PanelType.Monocrystalline);

            Action act = () => panel.RecordReading(Watts, Voltage);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*negative*");
        }


    }
}
