using System;
using System.Linq;
using Service.EducationApi.Models;
using Service.TutorialPersonal.Grpc.Models;

namespace Service.EducationApi.Mappers
{
	public static class PersonalTaskRequestMapper
	{
		public static PersonalTaskTextGrpcRequest ToGrpcModel(this TaskTextRequest model, Guid? userId) => new PersonalTaskTextGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = TimeSpan.FromMilliseconds(model.Duration)
		};

		public static PersonalTaskTestGrpcRequest ToGrpcModel(this TaskTestRequest model, Guid? userId) => new PersonalTaskTestGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = TimeSpan.FromMilliseconds(model.Duration),
			Answers = model.Answers.Select(answer => new PersonalTaskTestAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static PersonalTaskCaseGrpcRequest ToGrpcModel(this TaskCaseRequest model, Guid? userId) => new PersonalTaskCaseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = TimeSpan.FromMilliseconds(model.Duration),
			Value = model.Value
		};

		public static PersonalTaskTrueFalseGrpcRequest ToGrpcModel(this TaskTrueFalseRequest model, Guid? userId) => new PersonalTaskTrueFalseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = TimeSpan.FromMilliseconds(model.Duration),
			Answers = model.Answers.Select(answer => new PersonalTaskTrueFalseAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static PersonalTaskGameGrpcRequest ToGrpcModel(this TaskGameRequest model, Guid? userId) => new PersonalTaskGameGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = TimeSpan.FromMilliseconds(model.Duration)
		};
	}
}