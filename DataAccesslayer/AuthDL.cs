using EMPLOYEE_SIGNING_AND_SIGNUP_API.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EMPLOYEE_SIGNING_AND_SIGNUP_API.DataAccesslayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(this._configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<SignInResponce> SignIn(SignInRequest request)
        {
            SignInResponce responce = new SignInResponce();
            responce.IsSuccess = true;
            responce.Message = "Success";
            try
            {
                //if (!request.Password.Equals(request.ConfirmPassword))
                //{
                //    responce.IsSuccess = false;
                //    responce.Message = "Password and Confirm Password Not Match.";
                //    return responce;
                //}

                if (_sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _sqlConnection.OpenAsync();
                }

                string SqlQuery = @"select * from [dbo].[tbl_EmployeeDetails] where 
                                    Emp_Username=@Emp_Username and Emp_Password=@Emp_Password and Emp_Role=@Emp_Role";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Username", request.Username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Password", request.Password);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Role", request.Role);
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            responce.Message = "Login Successfull.";
                        }
                        else
                        {
                            responce.IsSuccess = false;
                            responce.Message = "Login Failed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.Message = ex.Message;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }
            return responce;
        }
        public async Task<SignUpResponce> SignUp(SignUpRequest request)
        {
            SignUpResponce responce = new SignUpResponce();
            responce.IsSuccess = true;
            responce.Message = "Success";
            try
            {
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    responce.IsSuccess = false;
                    responce.Message = "Password and Confirm Password Not Match.";
                    return responce;
                }

                if (_sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _sqlConnection.OpenAsync();
                }

                string SqlQuery = @"insert into [dbo].[tbl_EmployeeDetails](Emp_Username,Emp_Password,Emp_Role,InsertionDate)values
                                    (@Emp_Username, @Emp_Password, @Emp_Role, @InsertionDate)";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Username", request.Username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Password", request.Password);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Emp_Role", request.Role);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@InsertionDate", DateTime.Now.ToString("M/d/yyyy"));
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        responce.IsSuccess = false;
                        responce.Message = "Something went wrong";
                        return responce;
                    }
                }
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.Message = ex.Message;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }
            return responce;
        }
    }
}
