using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [SecuredOperation(Priority = 1)]
        [ValidationAspect(typeof(RegisterUserValidator), Priority = 2)]
       // [CacheRemoveAspect("Get")]
        [LogAspect(typeof(FileLogger))]
        public async Task<IResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

            if (isThereAnyUser != null)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

            HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
            var user = new User
            {
                Email = request.Email,

                FullName = request.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                IsUltra = request.IsUltra,
                IsSetra = request.IsSetra,
                IsBetkolik = request.IsBetkolik
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }
    }
}
