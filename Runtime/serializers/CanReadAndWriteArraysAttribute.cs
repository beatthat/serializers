using System;
namespace BeatThat.Serializers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CanReadAndWriteArraysAttribute : Attribute
    {
    }
}
