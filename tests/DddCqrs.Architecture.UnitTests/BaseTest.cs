using System.Reflection;
using DddCqrs.SharedKernel;

namespace DddCqrs.Architecture.UnitTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
}
