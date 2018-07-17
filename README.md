Serializers read and write typed objects, providing interfaces to read or write
an object to a user that shouldn't need to care about the data format.

## Install

From your unity project folder:

    npm init
    npm install beatthat/serializers --save

The package and all its dependencies will be installed under Assets/Plugins/packages/beatthat.

In case it helps, a quick video of the above: https://youtu.be/Uss_yOiLNw8

## USAGE

#### Read an item from a stream

The example below shows reading a single json item from a stream. In practice, you wouldn't bother with a Reader if you're doing all of this inline and just want to parse json. The Reader can be useful though in concert with something like a library for making HTTP requests and returning the result as an object @see https://github.com/beatthat/requests

TODO: change the example below to something that motivates why you would use a Reader, e.g. internal code from Requests

```c#
using BeatThat.Serializers;

public class MyClass
{
    [Serializable]
    public struct Data
    {
        public string name;
        public string type;
    }

    public const string JSON =
@"{
""name"":""my name"",
""type"":""my type"",
}";

    public Data ReadItem()
    {
      var reader = new JsonReader<Data>(); // in real apps, usually share a static instance
      using (var s = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
      {
          try
          {
              return reader.ReadOne(s);
          }
          catch (Exception e)
          {
              Debug.LogWarning("invalid json: " + e.Message);
          }
      }
    }

}
```

## SAMPLES

This package installs with a Samples that has a few basic examples
that demonstrate how to a reader.
