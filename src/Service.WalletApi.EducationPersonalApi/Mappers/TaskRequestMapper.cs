using System;
using System.Linq;
using Service.TutorialPersonal.Grpc.Models.Task;
using Service.WalletApi.EducationPersonalApi.Controllers.Contracts;

namespace Service.WalletApi.EducationPersonalApi.Mappers
{
	public static class TaskRequestMapper
	{
		public static TaskTextGrpcRequest ToGrpcModel(this TaskTextRequest model, Guid? userId, TimeSpan duration) => new TaskTextGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration
		};

		public static TaskTestGrpcRequest ToGrpcModel(this TaskTestRequest model, Guid? userId, TimeSpan duration) => new TaskTestGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Answers = model.Answers.Select(answer => new TaskTestAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static TaskVideoGrpcRequest ToGrpcModel(this TaskVideoRequest model, Guid? userId, TimeSpan duration) => new TaskVideoGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration
		};

		public static TaskCaseGrpcRequest ToGrpcModel(this TaskCaseRequest model, Guid? userId, TimeSpan duration) => new TaskCaseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Value = model.Value
		};

		public static TaskTrueFalseGrpcRequest ToGrpcModel(this TaskTrueFalseRequest model, Guid? userId, TimeSpan duration) => new TaskTrueFalseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Answers = model.Answers.Select(answer => new TaskTrueFalseAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static TaskGameGrpcRequest ToGrpcModel(this TaskGameRequest model, Guid? userId, TimeSpan duration) => new TaskGameGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Passed = model.Passed
		};
	}
}