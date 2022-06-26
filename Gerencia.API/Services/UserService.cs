namespace Gerencia.API.Services;

using AutoMapper;
using BCrypt.Net;
using Gerencia.API.Authorization;
using Gerencia.API.Entities;
using Gerencia.API.Helpers;
using Gerencia.API.Models.Users;

public interface IUserService
{
 AuthenticateResponse Authenticate(AuthenticateRequest model);
 IEnumerable<User> GetAll();
 User GetById(int id);
 void Register(RegisterRequest model);
 void Update(int id, UpdateRequest model);
 void Delete(int id);
}

public class UserService : IUserService
{
 private DataContext _context;
 private IJwtUtils _jwtUtils;
 private readonly IMapper _mapper;

 public UserService(
     DataContext context,
     IJwtUtils jwtUtils,
     IMapper mapper)
 {
  _context = context;
  _jwtUtils = jwtUtils;
  _mapper = mapper;
 }

 public AuthenticateResponse Authenticate(AuthenticateRequest model)
 {
  var user = _context.Users.SingleOrDefault(x => x.Login.Equals(model.Login));
  if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
   throw new AppException("Login or password is incorrect");

  // authentication successful
  var response = _mapper.Map<AuthenticateResponse>(user);
  response.Token = _jwtUtils.GenerateToken(user);
  return response;
 }

 public IEnumerable<User> GetAll()
 {
  return _context.Users;
 }

 public User GetById(int id)
 {
  return getUser(id);
 }

 public void Register(RegisterRequest model)
 {
  // validate
  if (_context.Users.Any(x => x.Login == model.Login))
   throw new AppException("Login '" + model.Login + "' is already taken");

  // map model to new user object
  var user = _mapper.Map<User>(model);
  user.RegistrationTime = DateTime.Now;

  // hash password
  user.PasswordHash = BCrypt.HashPassword(model.Password);

  // save user
  _context.Users.Add(user);
  _context.SaveChanges();
 }

 public void Update(int id, UpdateRequest model)
 {
  var user = getUser(id);

  // validate
  if (model.Login != user.Login && _context.Users.Any(x => x.Login == model.Login))
   throw new AppException("Login '" + model.Login + "' is already taken");

  // hash password if it was entered
  if (!string.IsNullOrEmpty(model.Password))
   user.PasswordHash = BCrypt.HashPassword(model.Password);

  // copy model to user and save
  _mapper.Map(model, user);
  _context.Users.Update(user);
  _context.SaveChanges();
 }

 public void Delete(int id)
 {
  var user = getUser(id);
  _context.Users.Remove(user);
  _context.SaveChanges();
 }

 // helper methods

 private User getUser(int id)
 {
  var user = _context.Users.Find(id);
  if (user == null) throw new KeyNotFoundException("User not found");
  return user;
 }
}