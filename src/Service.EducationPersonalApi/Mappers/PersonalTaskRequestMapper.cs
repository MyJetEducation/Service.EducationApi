using System;
using System.Linq;
using Service.EducationPersonalApi.Models;
using Service.TutorialPersonal.Grpc.Models;

namespace Service.EducationPersonalApi.Mappers
{
	public static class PersonalTaskRequestMapper
	{
		public static PersonalTaskTextGrpcRequest ToGrpcModel(this TaskTextRequest model, Guid? userId, TimeSpan duration) => new PersonalTaskTextGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration
		};

		public static PersonalTaskTestGrpcRequest ToGrpcModel(this TaskTestRequest model, Guid? userId, TimeSpan duration) => new PersonalTaskTestGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Answers = model.Answers.Select(answer => new PersonalTaskTestAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static PersonalTaskCaseGrpcRequest ToGrpcModel(this TaskCaseRequest model, Guid? userId, TimeSpan duration) => new PersonalTaskCaseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Value = model.Value
		};

		public static PersonalTaskTrueFalseGrpcRequest ToGrpcModel(this TaskTrueFalseRequest model, Guid? userId, TimeSpan duration) => new PersonalTaskTrueFalseGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration,
			Answers = model.Answers.Select(answer => new PersonalTaskTrueFalseAnswerGrpcModel
			{
				Number = answer.Number,
				Value = answer.Value
			}).ToArray()
		};

		public static PersonalTaskGameGrpcRequest ToGrpcModel(this TaskGameRequest model, Guid? userId, TimeSpan duration) => new PersonalTaskGameGrpcRequest
		{
			UserId = userId,
			IsRetry = model.IsRetry,
			Duration = duration
		};
	}
}