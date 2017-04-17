using System;
using System.Linq;

namespace BabysitterKat
{
    public class Program
    {
        static void Main(string[] args)
        {
        }
        public class BabySitterFeeCalculator
        {
            private TimeConverter _timeConverter;

            private const double MIDNIGHT = 24.0;
            private const double LASTENDTIME = 4.0;
            private const double EARLIESTSTARTTIME = 17.0;

            private const int BEFOREBEDRATE = 12;
            private const int AFTERBEDRATE = 8;
            private const int AFTERMIDRATE = 16;

            private double _doubleStartTime;
            private double _doubleEndTime;
            private double _doubleBedTime;

            private double doubleStartToBedFee = 0.0;
            private double doubleBedToMidFee = 0.0;
            private double doubleMidToEndFee = 0.0;

            private double _doubleFee;
            private int _fee;

            private enum Type
            {
                StartAndEndAfterMid,
                BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid,
                BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid,
                StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid,
                StartBeforeOrAtMidBedAndEndAfterMid,
                StartBeforeBedBothBeforeMidEndBeforeMid,
                StartBeforeBedBothBeforeMidEndAfterMid,
                defaultType
            }
            public BabySitterFeeCalculator()
            {
                _timeConverter = new TimeConverter();
            }
            private bool CheckStartTime(double doubleStartTime)
            {
                if (doubleStartTime >= EARLIESTSTARTTIME || doubleStartTime < LASTENDTIME)
                    return true;
                return false;
            }
            private bool CheckEndTime(double doubleEndTime, double doubleStartTime)
            {
                if ((doubleEndTime <= LASTENDTIME || doubleEndTime > EARLIESTSTARTTIME) && doubleEndTime != doubleStartTime)
                    return true;
                return false;
            }
            private bool CheckIsNoon(string time)
            {
                if (time.Equals("12:00PM"))
                {
                    return true;
                }
                return false;
            }
            private double CheckAndAssignStartTime(string startTime)
            {
                double doubleStartTime = 0.0;
                doubleStartTime = _timeConverter.ConvertTimeString(startTime);
                if (!CheckStartTime(doubleStartTime) || CheckIsNoon(startTime))
                {
                    throw new System.ArgumentException("Start time entered is incorrect.", startTime);
                }
                else
                    return doubleStartTime;
            }
            private double CheckAndAssignEndTime(string endTime, double doubleStartTime)
            {
                double doubleEndTime = 0.0;
                doubleEndTime = _timeConverter.ConvertTimeString(endTime);
                if (!CheckEndTime(doubleEndTime, doubleStartTime) || CheckIsNoon(endTime))
                {
                    throw new System.ArgumentException("End time entered is incorrect.", endTime);
                }
                else
                    return doubleEndTime;
            }
            private double CalculateDiffBetweenTwoTimes(double firstTimeDouble, double SecondTimeDouble)
            {
                double diff = 0;
                return diff = (firstTimeDouble - SecondTimeDouble) - Math.Floor(firstTimeDouble - SecondTimeDouble);
            }
            private void CalculateFracHoursStartAndEndTime(double doubleStartTime, double doubleEndTime)
            {
                _doubleStartTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
                _doubleEndTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
            }
            private void CalculateRateStartAndEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                if (_doubleStartTime < _doubleEndTime)
                {
                    _doubleFee = _doubleEndTime - _doubleStartTime;
                    _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERMIDRATE;
                }
                else if (_doubleStartTime > _doubleEndTime)
                {
                    _doubleFee = _doubleEndTime - (_doubleStartTime - MIDNIGHT);
                    _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERMIDRATE;
                }
            }
            private void CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                _doubleFee = _doubleEndTime - _doubleStartTime;
                _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERBEDRATE;
            }
            private void CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                if (CalculateDiffBetweenTwoTimes(_doubleStartTime, _doubleEndTime) == 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(_doubleFee) * AFTERBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERMIDRATE;
                    return;
                }
                if (CalculateDiffBetweenTwoTimes(MIDNIGHT, _doubleStartTime) > 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
                    return;
                }
                if ((_doubleEndTime) - Math.Floor(_doubleEndTime) > 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(_doubleFee) * AFTERBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERMIDRATE;
                }
            }
            private void CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(double doubleStartTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                _doubleFee = _doubleEndTime - _doubleStartTime;
                _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * BEFOREBEDRATE;
            }
            private void CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(double doubleStartTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                if (CalculateDiffBetweenTwoTimes(_doubleStartTime, _doubleEndTime) == 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(_doubleFee) * BEFOREBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
                    return;
                }
                if (CalculateDiffBetweenTwoTimes(MIDNIGHT, _doubleStartTime) > 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(Math.Ceiling(_doubleFee)) * BEFOREBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
                    return;
                }
                if ((_doubleEndTime) - Math.Floor(_doubleEndTime) > 0)
                {
                    _doubleFee = MIDNIGHT - _doubleStartTime;
                    _fee = Convert.ToInt32(_doubleFee) * BEFOREBEDRATE;
                    _doubleFee = _doubleEndTime;
                    _fee = _fee + Convert.ToInt32(Math.Ceiling(_doubleFee)) * AFTERMIDRATE;
                }
            }
            private void CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                _doubleBedTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
                int feeStartToBed = 0;
                int feeBedToEnd = 0;
                double doubleStartToBedFee = 0.0;
                double doubleBedToEndFee = 0.0;
                if ((CalculateDiffBetweenTwoTimes(_doubleBedTime, _doubleStartTime) > 0)
                    && (CalculateDiffBetweenTwoTimes(_doubleEndTime, _doubleBedTime) > 0))
                {
                    doubleStartToBedFee = _doubleBedTime - _doubleStartTime;
                    feeStartToBed = Convert.ToInt32(Math.Ceiling(doubleStartToBedFee)) * BEFOREBEDRATE;
                    doubleBedToEndFee = _doubleEndTime - _doubleBedTime;
                    feeBedToEnd = Convert.ToInt32(Math.Floor(doubleBedToEndFee)) * AFTERBEDRATE;
                    _fee = feeBedToEnd + feeStartToBed;
                    return;
                }
                if (CalculateDiffBetweenTwoTimes(_doubleBedTime, _doubleStartTime) == 0)
                {
                    doubleStartToBedFee = _doubleBedTime - _doubleStartTime;
                    feeStartToBed = Convert.ToInt32(doubleStartToBedFee) * BEFOREBEDRATE;
                }
                else if (CalculateDiffBetweenTwoTimes(_doubleBedTime, _doubleStartTime) > 0)
                {
                    doubleStartToBedFee = _doubleBedTime - _doubleStartTime;
                    feeStartToBed = Convert.ToInt32(Math.Ceiling(doubleStartToBedFee)) * BEFOREBEDRATE;
                }
                if (CalculateDiffBetweenTwoTimes(_doubleEndTime, _doubleBedTime) == 0)
                {
                    doubleBedToEndFee = _doubleEndTime - _doubleBedTime;
                    feeBedToEnd = Convert.ToInt32(doubleBedToEndFee) * AFTERBEDRATE;
                }
                else if (CalculateDiffBetweenTwoTimes(_doubleEndTime, _doubleBedTime) > 0)
                {
                    doubleBedToEndFee = _doubleEndTime - _doubleBedTime;
                    feeBedToEnd = Convert.ToInt32(Math.Ceiling(doubleBedToEndFee)) * AFTERBEDRATE;
                }
                _fee = feeBedToEnd + feeStartToBed;
            }
            private void CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {
                CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
                _doubleBedTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
                DoubleFeeWhenStartAndEndPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenStartBedAndEndPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenNoPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenBedPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenStartPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenStartAndBedPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenEndPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                DoubleFeeWhenBedAndEndPartialHours(_doubleStartTime, _doubleEndTime, _doubleBedTime);
                _doubleFee = doubleStartToBedFee + doubleBedToMidFee + doubleMidToEndFee;
                _fee = Convert.ToInt32(_doubleFee);
            }
            public int CalculateRate(string startTime, string bedTime, string endTime)
            {
                double doubleStartTime = 0;
                double doubleEndTime = 0;
                double doubleBedTime = 0;
                try
                {
                    doubleStartTime = CheckAndAssignStartTime(startTime);
                    doubleEndTime = CheckAndAssignEndTime(endTime, doubleStartTime);
                }
                catch
                {
                }
                doubleBedTime = _timeConverter.ConvertTimeString(bedTime);
                Type type = DetermineType(doubleStartTime, doubleBedTime, doubleEndTime);
                switch (type)
                {
                    case Type.StartAndEndAfterMid:
                        {
                            CalculateRateStartAndEndAfterMid(doubleStartTime, doubleEndTime);
                            break;
                        }
                    case Type.BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid:
                        {
                            CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(doubleStartTime, doubleEndTime);
                            break;
                        }
                    case Type.BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid:
                        {
                            CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(doubleStartTime, doubleEndTime);
                            break;
                        }
                    case Type.StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid:
                        {
                            CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(doubleStartTime, doubleEndTime);
                            break;
                        }
                    case Type.StartBeforeOrAtMidBedAndEndAfterMid:
                        {
                            CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(doubleStartTime, doubleEndTime);
                            break;
                        }
                    case Type.StartBeforeBedBothBeforeMidEndBeforeMid:
                        {
                            CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(doubleStartTime, doubleBedTime, doubleEndTime);
                            break;
                        }
                    case Type.StartBeforeBedBothBeforeMidEndAfterMid:
                        {
                            CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(doubleStartTime, doubleBedTime, doubleEndTime);
                            break;
                        }
                    default:
                        return _fee;
                }
                return _fee;
            }
            private Type DetermineType(double doubleStartTime, double doubleBedTime, double doubleEndTime)
            {
                Type type;
                if (doubleEndTime <= LASTENDTIME && (doubleStartTime < LASTENDTIME || doubleStartTime > MIDNIGHT))
                    return type = Type.StartAndEndAfterMid;

                else if (doubleStartTime >= doubleBedTime && doubleStartTime < doubleEndTime && doubleEndTime > EARLIESTSTARTTIME
                    && doubleStartTime < MIDNIGHT && doubleBedTime > LASTENDTIME)
                    return type = Type.BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid;

                else if (doubleStartTime >= doubleBedTime && doubleStartTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleBedTime > LASTENDTIME)
                    return type = Type.BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid;

                else if (doubleStartTime < doubleEndTime && doubleEndTime <= MIDNIGHT && doubleStartTime < MIDNIGHT
                    && doubleStartTime > LASTENDTIME && (doubleBedTime >= doubleEndTime || doubleBedTime <= LASTENDTIME))
                    return type = Type.StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid;

                else if (doubleStartTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleBedTime <= LASTENDTIME)
                    return type = Type.StartBeforeOrAtMidBedAndEndAfterMid;

                else if (doubleStartTime < doubleBedTime && doubleBedTime < doubleEndTime && doubleEndTime <= MIDNIGHT)
                    return type = Type.StartBeforeBedBothBeforeMidEndBeforeMid;

                else if (doubleStartTime < doubleBedTime && doubleBedTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleStartTime >= EARLIESTSTARTTIME)
                    return type = Type.StartBeforeBedBothBeforeMidEndAfterMid;

                else
                    return type = Type.defaultType;
            }
            private double CalculateDiffToDeterminePartOfHour(double doubleTime)
            {
                double diff = 0;
                return diff = doubleTime - Math.Floor(doubleTime);
            }
            private void DoubleFeeWhenStartAndEndPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                {
                    doubleStartToBedFee = Math.Floor(doubleBedTime - doubleStartTime) * BEFOREBEDRATE;
                    doubleBedToMidFee = (MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = Math.Ceiling(doubleEndTime) * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenStartBedAndEndPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                {
                    doubleStartToBedFee = (Math.Floor(doubleBedTime) - Math.Floor(doubleStartTime)) * BEFOREBEDRATE;
                    doubleBedToMidFee = Math.Floor(MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = Math.Ceiling(doubleEndTime) * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenNoPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                {
                    doubleStartToBedFee = (doubleBedTime - doubleStartTime) * BEFOREBEDRATE;
                    doubleBedToMidFee = (MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = doubleEndTime * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenBedPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                {
                    doubleStartToBedFee = (Math.Ceiling(doubleBedTime - doubleStartTime)) * BEFOREBEDRATE;
                    doubleBedToMidFee = Math.Floor(MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = doubleEndTime * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenStartPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                {
                    doubleStartToBedFee = Math.Ceiling(doubleBedTime - doubleStartTime) * BEFOREBEDRATE;
                    doubleBedToMidFee = (MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = doubleEndTime * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenStartAndBedPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) > 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) == 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                {
                    doubleStartToBedFee = (Math.Floor(doubleBedTime) - Math.Floor(doubleStartTime)) * BEFOREBEDRATE;
                    doubleBedToMidFee = Math.Ceiling(MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = doubleEndTime * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenEndPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) == 0)
                {
                    doubleStartToBedFee = (doubleBedTime - doubleStartTime) * BEFOREBEDRATE;
                    doubleBedToMidFee = (MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = Math.Ceiling(doubleEndTime) * AFTERMIDRATE;
                }
            }
            private void DoubleFeeWhenBedAndEndPartialHours(double doubleStartTime, double doubleEndTime, double doubleBedTime)
            {
                if (CalculateDiffToDeterminePartOfHour(doubleStartTime) == 0 && CalculateDiffToDeterminePartOfHour(doubleEndTime) > 0
                    && CalculateDiffToDeterminePartOfHour(doubleBedTime) > 0)
                {
                    doubleStartToBedFee = (Math.Ceiling(doubleBedTime - doubleStartTime)) * BEFOREBEDRATE;
                    doubleBedToMidFee = Math.Floor(MIDNIGHT - doubleBedTime) * AFTERBEDRATE;
                    doubleMidToEndFee = Math.Ceiling(doubleEndTime) * AFTERMIDRATE;
                }
            }
        }
        public class Utility
        {
            public char[] MakeCharArray(string time)
            {
                char[] timeArray = new char[time.Length];
                return timeArray = time.ToCharArray();
            }
            public bool CheckAmOrPm(string time)
            {
                if (time.EndsWith("PM"))
                {
                    return true;
                }
                return false;
            }
            public char[] RemoveColon(char[] timeArray)
            {
                int i = 0;
                for (; i < timeArray.Length; ++i)
                {
                    if (timeArray[i] == ':')
                        timeArray[i] = '.';
                }
                return timeArray;
            }
            public char[] RemoveAmPm(char[] timeArray)
            {
                timeArray = timeArray.Where(val => val != 'A').ToArray();
                timeArray = timeArray.Where(val => val != 'P').ToArray();
                timeArray = timeArray.Where(val => val != 'M').ToArray();
                return timeArray;
            }
        }
        public class TimeConverter
        {
            private Utility _utility;
            public TimeConverter()
            {
                _utility = new Utility();
            }
            public double ConvertTimeArrayToDouble(char[] timeArray)
            {
                double timeDouble = 0.0;
                string time = new string(timeArray);
                timeDouble = Double.Parse(time);
                return timeDouble;
            }
            public double MakeMilitaryTime(double doubleTimeDec, bool isPm)
            {
                if (!isPm && doubleTimeDec >= 12.0)
                    return doubleTimeDec + 12.0;
                if (isPm && doubleTimeDec < 12.0)
                    return doubleTimeDec + 12.0;
                return doubleTimeDec;
            }
            public double ConvertTimeString(string time)
            {
                double convertedDoubleTime = 0.0;
                time = time.ToUpper();
                bool isPm = _utility.CheckAmOrPm(time);
                char[] timeArray = _utility.MakeCharArray(time);
                timeArray = _utility.RemoveAmPm(timeArray);
                timeArray = _utility.RemoveColon(timeArray);
                convertedDoubleTime = ConvertTimeArrayToDouble(timeArray);
                convertedDoubleTime = MakeMilitaryTime(convertedDoubleTime, isPm);
                return convertedDoubleTime;
            }
            public double ConvertRawTimeDoubleToFractionalHours(double doubleTime)
            {
                double floorTime = 0.0;
                double fracTime = 0.0;
                floorTime = Math.Floor(doubleTime);
                fracTime = doubleTime - floorTime;
                doubleTime = Math.Round(((fracTime / .60) + floorTime), 2);
                return doubleTime;
            }
        }
    }
}

