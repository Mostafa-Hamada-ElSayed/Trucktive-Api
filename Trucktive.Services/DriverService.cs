using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Contracts.Drivers;
using Trucktive.Core.Entities;
using Trucktive.Core.Repositories;
using Trucktive.Core.Services;
using Trucktive.Core.Specifications.Drivers;

namespace Trucktive.Services
{
    public class DriverService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<int> AddDriverAsync(CreateDriverRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }
            
            var IdUser = await _userManager.FindByIdAsync(request.UserId);

            var supervisors = _unitOfWork.Repository<Supervisor>().GetAllAsQueryable();

            var supervisor = supervisors.FirstOrDefault(s => s.Email == IdUser!.Email);

            if (supervisor == null)
            {
                throw new Exception("Supervisor not found");
            }

            var driver = new Driver
            {
                FName = request.FName,
                LName = request.LName,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                SupervisorId = supervisor.Id
            };
          
            _unitOfWork.Repository<Driver>().Add(driver);

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

            if (!await _roleManager.RoleExistsAsync("Driver"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Driver"));
            }

            await _userManager.AddToRoleAsync(user, "Driver");

            return driver.Id;
        }

        public async Task DeleteDriverAsync(int id, CancellationToken cancellationToken = default)
        {
            var driver = await _unitOfWork.Repository<Driver>().GetByIdAsync(id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            _unitOfWork.Repository<Driver>().Delete(driver);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<DriverResponse> GetDriverByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new DriverWithSupervisorSpecification(id);

            var driver = await _unitOfWork.Repository<Driver>().GetByIdWithSpecAsync(spec);

            return driver == null
                ? throw new Exception("Driver not found")
                : new DriverResponse
                {
                    Id = driver.Id,
                    FName = driver.FName,
                    LName = driver.LName,
                    Address = driver.Address,
                    Phone = driver.Phone,
                    Email = driver.Email,
                    SupervisorId = driver.SupervisorId,
                    SupervisorName = driver.Supervisor?.FName + " " + driver.Supervisor?.LName
                };
        }

        public async Task<IReadOnlyList<DriverResponse>> GetDriversAsync(CancellationToken cancellationToken = default)
        {
            var spec = new DriverWithSupervisorSpecification();

            var drivers = await _unitOfWork.Repository<Driver>().GetAllWithSpecAsync(spec);

            return drivers.Select(driver => new DriverResponse
            {
                Id = driver.Id,
                FName = driver.FName,
                LName = driver.LName,
                Address = driver.Address,
                Phone = driver.Phone,
                Email = driver.Email,
                SupervisorId = driver.SupervisorId,
                SupervisorName = driver.Supervisor?.FName + " " + driver.Supervisor?.LName

            }).ToList().AsReadOnly();
        }

        public async Task<DriverResponse> UpdateDriverAsync(int id, UpdateDriverRequest request, CancellationToken cancellationToken = default)
        {
            var driver = await _unitOfWork.Repository<Driver>().GetByIdAsync(id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            } 

            driver.FName = request.FName;
            driver.LName = request.LName;
            driver.Address = request.Address;
            driver.Phone = request.Phone;
            //driver.Email = request.Email;
            //driver.SupervisorId = request.SupervisorId;


            _unitOfWork.Repository<Driver>().Update(driver);

            await _unitOfWork.CompleteAsync();

            return new DriverResponse
            {
                Id = driver.Id,
                FName = driver.FName,
                LName = driver.LName,
                Address = driver.Address,
                Phone = driver.Phone,
                Email = driver.Email,
                SupervisorId = driver.SupervisorId,
                SupervisorName = driver.Supervisor?.FName + " " + driver.Supervisor?.LName
            };
        }
    }
}
