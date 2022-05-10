using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace CalculTests;

[TestFixture]
public class Test
{
    public IEnumerable<int> Foo()
    {
        foreach (var i in Bar())
        {
            yield return i + 10;
        }

        foreach (var i in Kek())
        {
            yield return i + 20;
        }
    }

    public IEnumerable<int> Bar()
    {
        yield return 1;
        yield return 2;
        yield break;
        yield return 3;
    }

    public IEnumerable<int> Kek()
    {
        yield return 4;
        yield return 5;
        yield break;
        yield return 6;

    }

    [Test]
    public void Tst()
    {
        foreach (var i in Foo())
        {
            Console.Out.WriteLine(i);
        }
    }
}