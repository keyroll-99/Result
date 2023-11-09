using FluentAssertions;
using NSubstitute;
using Result.Test.SampleModel;
using Xunit;

namespace Result.Test;

public class ResultTests
{
    private readonly Action<User> _successCallback = Substitute.For<Action<User>>();
    private readonly Action<Exception> _errorCallback = Substitute.For<Action<Exception>>();
    private readonly Func<User, string> _successCallbackWithResponse = Substitute.For<Func<User, string>>();
    private readonly Func<Exception, string> _errorCallbackWithResponse = Substitute.For<Func<Exception, string>>();
    
    [Fact]
    public void When_Result_Is_Success_Match_Should_Call_Success_Callback_And_Model_Should_Keep_Success_Model()
    {
        // Arrange
        var result = Result<User, Exception>.Success(new User
        {
            Name = "Name",
            Surname = "Surname",
            Id = Guid.NewGuid()
        });
        
        // Act
        result.OnSuccess(_successCallback).OnError(_errorCallback);
        result.Match(_successCallbackWithResponse, _errorCallbackWithResponse);
        User? resultSuccessModel = result;
        Exception? errorModel = result;
        
        // Assert
        _successCallbackWithResponse.Received(1).Invoke(Arg.Any<User>());
        _errorCallbackWithResponse.DidNotReceive().Invoke(Arg.Any<Exception>()); 
        _successCallback.Received(1).Invoke(Arg.Any<User>());
        _errorCallback.DidNotReceive().Invoke(Arg.Any<Exception>());
        result.IsSuccess.Should().BeTrue();
        resultSuccessModel.Should().NotBeNull();
        errorModel.Should().BeNull();
    }

    [Fact]
    public void When_Result_Is_Error_Match_Should_Call_Error_Callback_And_Model_Should_Keep_Error_Model()
    {
        // Arrange
        var result = Result<User, Exception>.Fail(new Exception("error"));
        
        // Act
        result.OnSuccess(_successCallback).OnError(_errorCallback);
        result.Match(_successCallbackWithResponse, _errorCallbackWithResponse);
        User? resultSuccessModel = result;
        Exception? errorModel = result;
        
        // Assert
        _successCallbackWithResponse.DidNotReceive().Invoke(Arg.Any<User>());
        _errorCallbackWithResponse.Received(1).Invoke(Arg.Any<Exception>()); 
        _errorCallback.Received(1).Invoke(Arg.Any<Exception>());
        _successCallback.DidNotReceive().Invoke(Arg.Any<User>());
        result.IsSuccess.Should().BeFalse();
        resultSuccessModel.Should().BeNull();
        errorModel.Should().NotBeNull();
    }
}