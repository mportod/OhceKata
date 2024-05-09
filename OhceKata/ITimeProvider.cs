using System;

namespace OhceKata;

public interface IClock
{
    TimeOnly GetCurrentHour();
}