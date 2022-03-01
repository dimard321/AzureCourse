using AutoFixture;
using AutoMapper;
using CST.Tests.Common;
using Moq;
using Moq.AutoMock;

namespace Pr.API.UnitTests;

public class AutoMockerTestsBase<TTarget>
	where TTarget : class
{
	private readonly AutoMocker _mocker = new();

	private TTarget _target;
	public TTarget Target
	{
		get
		{
			if (_target is null)
				_target = _mocker.CreateInstance<TTarget>();

			return _target;
		}
	}

	protected Fixture Fixture { get; }

	public AutoMockerTestsBase()
	{
		Fixture = FixtureInitializer.InitializeFixture();
	}

	public Mock<T> GetMock<T>() where T : class =>
		_mocker.GetMock<T>();

	public T GetService<T>() where T : class =>
		_mocker.Get<T>();

	public void Use<T>(T instance) where T : class
	{
		_mocker.Use(instance);
	}

	public IMapper UseMapperWithProfiles(params Profile[] profiles)
	{
		var mapper = new Mapper(new MapperConfiguration(cfg =>
		{
			cfg.AddProfiles(profiles);
		}));

		Use<IMapper>(mapper);

		return mapper;
	}
}