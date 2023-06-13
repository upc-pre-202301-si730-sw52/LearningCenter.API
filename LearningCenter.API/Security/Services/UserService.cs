using AutoMapper;
using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Domain.Services.Communication;
using LearningCenter.API.Security.Exceptions;
using LearningCenter.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace LearningCenter.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }


    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
        var user = await _userRepository.FindByUsernameAsync(model.Username);
        Console.WriteLine($"Request: {model.Username}, {model.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName}, {user.Username}, {user.PasswordHash}");
        
        // Validate
        if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
        Console.WriteLine("Authentication successful. About to generate token");
        // Authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new AppException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest model)
    {
        // Validate
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException($"Username '{model.Username}'is already taken");
        
        // Map model to new user object
        var user = _mapper.Map<User>(model);
        Console.WriteLine($"User id: {user.Id}");
        
        // Hash password
        user.PasswordHash = BCryptNet.HashPassword(model.Password);
        
        // Save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest model)
    {
        var user = GetById(id);
        
        // Validate
        var existingUserWithName = await _userRepository.FindByUsernameAsync(model.Username);
        if (existingUserWithName != null && !existingUserWithName.Id.Equals(id))
            throw new AppException($"Username '{model.Username}' is already taken");
        
        // Hash password if it was entered
        if(!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCryptNet.HashPassword(model.Password);
        
        // Copy model to user and save
        _mapper.Map(model, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");

        }
    }

    public Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            return _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}