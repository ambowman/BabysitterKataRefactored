using Shouldly;
using Xunit;

namespace Tests
{
    public class Tests
    {
        BabysitterKat.Program.BabySitterFeeCalculator babySitterFeeCalculator = new BabysitterKat.Program.BabySitterFeeCalculator();
        
        [Fact]
        public void WhenCalculateRateGivenStartTime1AmBedTime10PmAndEndTime2AmShouldReturn16()
        {
            var result = babySitterFeeCalculator.CalculateRate("1:00AM", "10:00PM", "2:00AM");
            result.ShouldBe(16);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime2AmBedTime1AmAndEndTime3AmShouldReturn16()
        {
            var result = babySitterFeeCalculator.CalculateRate("2:00AM", "1:00AM", "3:00AM");
            result.ShouldBe(16);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime1AmBedTime1AmAndEndTime3AmShouldReturn32()
        {
            var result = babySitterFeeCalculator.CalculateRate("1:00AM", "1:00AM", "3:00");
            result.ShouldBe(32);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime1AmBedTime2AmAndEndTime3AmShouldReturn32()
        {
            var result = babySitterFeeCalculator.CalculateRate("1:00AM", "2:00AM", "3:00");
            result.ShouldBe(32);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime1AmBedTIme3AmEndTime3AmShouldReturn32()
        {
            var result = babySitterFeeCalculator.CalculateRate("1:00AM", "3:00AM", "3:00");
            result.ShouldBe(32);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime5PmBedTime3PmAndEndTime11PmShouldReturn48()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "3:00PM", "11:00PM");
            result.ShouldBe(48);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime5PmBedTime5PmAndEndTime11PmShouldReturn48()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "5:00PM", "11:00PM");
            result.ShouldBe(48);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime5PmBedTime3PmAndEndTime1AMmShouldReturn72()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "3:00PM", "1:00AM");
            result.ShouldBe(72);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime5PmBedTime5PmAndEndTime1AMmShouldReturn72()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "5:00PM", "1:00AM");
            result.ShouldBe(72);
        }
        [Fact]
        public void WhenCalculateRateGivenAStartTime5PmBedTime11PmAndEndTime11PmShouldReturn72()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "11:00PM", "11:00PM");
            result.ShouldBe(72);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PmBedTime1AmAndEndTime11PmShouldReturn72()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "1:00AM", "11:00PM");
            result.ShouldBe(72);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PmBedTime1AmAndEndTime1AmShouldReturn100()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "1:00AM", "1:00AM");
            result.ShouldBe(100);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTIme10PmBedTime1AmAndEndTIme2AmShouldReturn56()
        {
            var result = babySitterFeeCalculator.CalculateRate("10:00PM", "1:00AM", "2:00AM");
            result.ShouldBe(56);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PmBedTime2AmAndEndTime1AmShouldReturn100()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "2:00AM", "1:00AM");
            result.ShouldBe(100);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PmBedTime6PmEndTime11PmShouldReturn52()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "6:00PM", "11:00PM");
            result.ShouldBe(52);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTIme5PmBedTime6PmEndTime1AmShouldReturn76()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "6:00PM", "1:00AM");
            result.ShouldBe(76);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTimeOFMidnightBedTime10PmAndEndTime3AmShouldReturn48()
        {
            var result = babySitterFeeCalculator.CalculateRate("12:00AM", "10:00PM", "3:00AM");
            result.ShouldBe(48);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTimeOfMidnightBedTime1AmAndEndTime3AmShouldReturn48()
        {
            var result = babySitterFeeCalculator.CalculateRate("12:00AM", "1:00AM", "3:00AM");
            result.ShouldBe(48);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime10PmAndEnTimedMidnightShouldReturn76()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "10:00PM", "12:00AM");
            result.ShouldBe(76);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime1AmAndEndTimeMidnightShouldReturn84()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "1:00AM", "12:00AM");
            result.ShouldBe(84);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime10PMBedTimeMidnightAndEndTime1AmShouldReturn40()
        {
            var result = babySitterFeeCalculator.CalculateRate("10:00PM", "12:00AM", "1:00AM");
            result.ShouldBe(40);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime10PMBedTimeofMidnightAndEndTimeMidnightShouldReturn24()
        {
            var result = babySitterFeeCalculator.CalculateRate("10:00PM", "12:00AM", "12:00AM");
            result.ShouldBe(24);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTIme5PMBedTime5PmAndEndTimeOfMidnightShouldReturn56()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "5:00PM", "12:00AM");
            result.ShouldBe(56);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime3PmAndEndTime11PMShouldReturn48()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "3:00PM", "11:00PM");
            result.ShouldBe(48);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime12ThirtyAMBedTime3PmAndEndTime4AMShouldReturn64()
        {
            var result = babySitterFeeCalculator.CalculateRate("12:30AM", "3:00PM", "4:00AM");
            result.ShouldBe(64);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime3PmEndTime1AMShouldReturn72()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "3:00PM", "1:00AM");
            result.ShouldBe(72);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime3PmAndEndTime1ThirtyAMShouldReturn80()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "3:00PM", "1:30AM");
            result.ShouldBe(80);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime3PmAndEndTime1ThirtyAMShouldReturn88()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "3:00PM", "1:30AM");
            result.ShouldBe(88);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime1AmAndEndTime1AMShouldReturn100()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "1:00AM", "1:00AM");
            result.ShouldBe(100);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime1AmAndEndTime1ThirtyAMShouldReturn104()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "1:00AM", "1:30AM");
            result.ShouldBe(104);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime1AmAndEndTime1ThirtyAMShouldReturn116()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "1:00AM", "1:30AM");
            result.ShouldBe(116);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7PmAndEndTime11PMShouldReturn56()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:00PM", "11:00PM");
            result.ShouldBe(56);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7ThirtyPmAndEndTime11PMShouldReturn56()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:30PM", "11:00PM");
            result.ShouldBe(56);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7ThirtyPmAndEndTime11ThirtyPMShouldReturn56()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:30PM", "11:30PM");
            result.ShouldBe(56);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime11ThirtyPMShouldReturn68()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "11:30PM");
            result.ShouldBe(68);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7PmAndEndTime11ThirtyPMShouldReturn64()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:00PM", "11:30PM");
            result.ShouldBe(64);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime11PMShouldReturn60()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "11:00PM");
            result.ShouldBe(60);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime11FifteenPMShouldReturn60()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "11:15PM");
            result.ShouldBe(60);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime11FortyFivePMShouldReturn68()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "11:45PM");
            result.ShouldBe(68);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7PmAndEndTime1ThirtyPMShouldReturn84()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:00PM", "1:30AM");
            result.ShouldBe(84);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7ThirtyPmAndEndTime1ThirtyPMShouldReturn88()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:30PM", "1:30AM");
            result.ShouldBe(88);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7PmAndEndTime1PMShouldReturn80()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:00PM", "1:00AM");
            result.ShouldBe(80);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime1PMShouldReturn84()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "1:00AM");
            result.ShouldBe(84);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7PmAndEndTime1PMShouldReturn80()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:00PM", "1:00AM");
            result.ShouldBe(80);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5ThirtyPMBedTime7ThirtyPmAndEndTime1PMShouldReturn80()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:30PM", "7:00PM", "1:00AM");
            result.ShouldBe(80);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7PmEndTime1ThirtyPMShouldReturn96()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:00PM", "1:30AM");
            result.ShouldBe(96);
        }
        [Fact]
        public void WhenCalculateRateGivenStartTime5PMBedTime7ThirtyPmAndEndTime1ThirtyPMShouldReturn100()
        {
            var result = babySitterFeeCalculator.CalculateRate("5:00PM", "7:30PM", "1:30AM");
            result.ShouldBe(100);
        }

    }
    public class UtilityAndTimeConverterTests
    {
        BabysitterKat.Program.Utility utility = new BabysitterKat.Program.Utility();
        BabysitterKat.Program.TimeConverter timeConverter = new BabysitterKat.Program.TimeConverter();

        public void MakeStringIntoArray()
        {
            string timeString = "5:00AM";
            var result = utility.MakeCharArray(timeString);
            result[0].ShouldBeOfType<char>();
            result[5].ShouldBeOfType<char>();
            result[0].ShouldBe('5');
        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithPm()
        {
            string timeString = "5:00PM";
            var result = utility.CheckAmOrPm(timeString);
            result.ShouldBe(true);

        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithAm()
        {
            string timeString = "5:00AM";
            var result = utility.CheckAmOrPm(timeString);
            result.ShouldBe(false);

        }
        [Fact]
        public void RemoveAmFromCharArray()
        {
            string timeString = "5:00AM";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemovePmFromCharArray()
        {
            string timeString = "5:00PM";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemoveColonFromCharArray()
        {
            string timeString = "5:00";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveColon(timeArray);
            result.ShouldBe("5.00");
        }
        [Fact]
        public void ConvertTimeArrayToDouble()
        {
            string timeString = "5.00";
            var timeArray = utility.MakeCharArray(timeString);
            var result = timeConverter.ConvertTimeArrayToDouble(timeArray);
            result.ShouldBe(5.00);
        }
        [Fact]
        public void MakeMilitaryTimeMid()
        {
            bool isPm = false;
            var result = timeConverter.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(24.00);
        }
        [Fact]
        public void MakeMilitaryTimeNoon()
        {
            bool isPm = true;
            var result = timeConverter.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(12.00);
        }
        [Fact]
        public void MakeMilitaryTimeAm()
        {
            bool isPm = false;
            var result = timeConverter.MakeMilitaryTime(11.00, isPm);
            result.ShouldBe(11.00);
        }
        [Fact]
        public void MakeMilitaryTimePm()
        {
            bool isPm = true;
            var result = timeConverter.MakeMilitaryTime(1.00, isPm);
            result.ShouldBe(13.00);
        }
        [Fact]
        public void ConvertTimeString()
        {
            var result = timeConverter.ConvertTimeString("5:00PM");
            result.ShouldBe(17.00);
        }
    }
}
