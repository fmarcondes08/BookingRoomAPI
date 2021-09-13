using AutoMapper;
using BookingRoomAPI.Application.Dtos.User;
using BookingRoomAPI.Application.Service.Interfaces;
using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingRoomAPI.Application.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserOutputDto> CreateAsync(CreateUserInputDto userDto)
        {
            var userMap = _mapper.Map<User>(userDto);

            var userCreated = await _userRepository.Add(userMap);

            return _mapper.Map<UserOutputDto>(userCreated);
        }

        public async Task<UserOutputDto> UpdateAsync(UpdateUserInputDto userDto)
        {
            var userMap = _mapper.Map<User>(userDto);

            var userCreated = await _userRepository.Update(userMap);

            return _mapper.Map<UserOutputDto>(userCreated);
        }

        public async Task<UserOutputDto> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserOutputDto>(user);
        }
    }
}
