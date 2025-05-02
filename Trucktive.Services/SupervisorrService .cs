using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Contracts.Drivers;
using Trucktive.Core.Contracts.Supervisor;
using Trucktive.Core.Entities;
using Trucktive.Core.Repositories;
using Trucktive.Core.Services;
using Trucktive.Core.Specifications.Drivers;

namespace Trucktive.Services
{
    public class SupervisorrService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISupervisorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;


        public async Task<int> AddSupervisorAsync(CreateSupervisorRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var supervisor = new Supervisor
            {
                FName = request.FName,
                LName = request.LName,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            _unitOfWork.Repository<Supervisor>().Add(supervisor);
            await _unitOfWork.CompleteAsync();

            var user = new ApplicationUser
            {
                FirstName = request.FName,
                LastName = request.LName,
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user account");
            }

            if (!await _roleManager.RoleExistsAsync("Supervisor"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
            }

            await _userManager.AddToRoleAsync(user, "Supervisor");

            return supervisor.Id;
        }


        public async Task DeleteSupervisorAsync(int id, CancellationToken cancellationToken = default)
        {
            var supervisor = await _unitOfWork.Repository<Supervisor>().GetByIdAsync(id);

            if (supervisor == null)
            {
                throw new Exception("Supervisor not found");
            }

            _unitOfWork.Repository<Supervisor>().Delete(supervisor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IReadOnlyList<SupervisorResponse>> GetSupervisorsAsync(CancellationToken cancellationToken = default)
        {
            var supervisors = await _unitOfWork.Repository<Supervisor>().GetAllAsync();

            return supervisors.Select(supervisor => new SupervisorResponse
            {
                Id = supervisor.Id,
                FName = supervisor.FName,
                LName = supervisor.LName,
                Address = supervisor.Address,
                Phone = supervisor.Phone,
                Email = supervisor.Email
            }).ToList().AsReadOnly();
        }

        public async Task<SupervisorResponse> GetSupervisorByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var supervisor = await _unitOfWork.Repository<Supervisor>().GetByIdAsync(id);

            if (supervisor == null)
            {
                throw new Exception("Supervisor not found");
            }

            return new SupervisorResponse
            {
                Id = supervisor.Id,
                FName = supervisor.FName,
                LName = supervisor.LName,
                Address = supervisor.Address,
                Phone = supervisor.Phone,
                Email = supervisor.Email
            };
        }

        public async Task<SupervisorResponse> UpdateSupervisorAsync(int id, UpdateSupervisorRequest request, CancellationToken cancellationToken = default)
        {
            var supervisor = await _unitOfWork.Repository<Supervisor>().GetByIdAsync(id);

            if (supervisor == null)
            {
                throw new Exception("Supervisor not found");
            }

            supervisor.FName = request.FName;
            supervisor.LName = request.LName;
            supervisor.Address = request.Address;
            supervisor.Phone = request.Phone;
            supervisor.Email = request.Email;

            _unitOfWork.Repository<Supervisor>().Update(supervisor);
            await _unitOfWork.CompleteAsync();

            return new SupervisorResponse
            {
                Id = supervisor.Id,
                FName = supervisor.FName,
                LName = supervisor.LName,
                Address = supervisor.Address,
                Phone = supervisor.Phone,
                Email = supervisor.Email
            };
        }
    } }
