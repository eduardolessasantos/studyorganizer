using FluentValidation;
using StudyOrganizer.Api.Dtos;

namespace StudyOrganizer.Api.Validators
{
    public class SubtopicCreateValidator : AbstractValidator<SubtopicCreateDto>
    {
        public SubtopicCreateValidator()
        {
            RuleFor(x => x.DisciplineId).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.MasteryLevel).InclusiveBetween(1, 5).When(x => x.MasteryLevel.HasValue);
            RuleFor(x => x.MaterialUrl).MaximumLength(2048).When(x => !string.IsNullOrWhiteSpace(x.MaterialUrl));
        }
    }


    public class SubtopicUpdateValidator : AbstractValidator<SubtopicUpdateDto>
    {
        public SubtopicUpdateValidator()
        {
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.Description != null);
            RuleFor(x => x.MasteryLevel).InclusiveBetween(1, 5).When(x => x.MasteryLevel.HasValue);
        }
    }
}
