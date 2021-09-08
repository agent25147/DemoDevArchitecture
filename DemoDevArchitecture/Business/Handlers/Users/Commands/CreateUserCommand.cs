using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Constants;
using MediatR;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
       
        public int UserId { get; set; }
        public long CitizenId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobilePhones { get; set; }
        public bool Status { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime RecordDate { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime UpdateContactDate { get; set; }
        public string Password { get; set; }
        public bool IsSetra { get; set; }
        public bool IsUltra { get; set; }
        public bool IsBetkolik { get; set; }


        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
        {
            private IUserRepository _userRepository;
           
            public CreateUserCommandHandler(IUserRepository userRepository  )
            {
                _userRepository = userRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (isThereAnyUser != null)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }

                var user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Status = true,
                    Address = request.Address,
                    BirthDate = request.BirthDate,
                    CitizenId = request.CitizenId,
                    Gender = request.Gender,
                    Notes = request.Notes,
                    MobilePhones = request.MobilePhones,
                    IsSetra = request.IsSetra,
                    IsBetkolik = request.IsBetkolik,
                    IsUltra = request.IsUltra
                };

             
                if (request.IsSetra)
                {
                    _userRepository.UseDb(SiteNames.Setra);
                    _userRepository.Add(user);

                    user.UserId = 0;
                    await _userRepository.SaveChangesAsync();
                }
                if (request.IsUltra)
                {
                    _userRepository.UseDb(SiteNames.Ultra);
                    _userRepository.Add(user);

                    user.UserId = 0;
                    await _userRepository.SaveChangesAsync();
                }
                if (request.IsBetkolik)
                {
                    _userRepository.UseDb(SiteNames.Betkolik);
                    _userRepository.Add(user);

                    user.UserId = 0;
                    await _userRepository.SaveChangesAsync();
                }

                // user is not admin and haven't selected any checkboxes
                if(!request.IsSetra && !request.IsSetra && !request.IsBetkolik)
                {
                    _userRepository.Add(user);
                    await _userRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
