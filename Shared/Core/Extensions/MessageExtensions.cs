using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class MessageExtensions
    {

        #region Created
        public static string Created(string entity)
        {
            var value = $"{entity} created.";
            return value;
        }
        #endregion
        #region Changed
        public static string Changed(string entity)
        {
            var value = $"{entity} changed.";
            return value;
        }
        #endregion
        #region NotCreated
        public static string NotCreated(string entity)
        {
            var value = $"{entity} not created !";
            return value;
        }
        #endregion
        #region NotChanged
        public static string NotChanged(string entity)
        {
            var value = $"{entity} not changed";
            return value;
        }
        #endregion
        #region Completed
        public static string Completed(string entity)
        {
            var value = $"{entity} completed";
            return value;
        }
        #endregion
        #region NotCompleted
        public static string NotCompleted(string entity)
        {
            var value = $"{entity} not completed";
            return value;
        }
        #endregion
        #region NotCorrect
        public static string NotCorrect(string entity)
        {
            var value = $"{entity} not correct";
            return value;
        }
        #endregion
        #region Correct
        public static string Correct(string entity)
        {
            var value = $"{entity} correct";
            return value;
        }
        #endregion

        #region Added
        public static string Added(string entity)
        {
            var value = $"{entity} added";
            return value;
        }
        #endregion
        #region SavedOrUpdated
        public static string SavedOrUpdated(string entity)
        {
            var value = $"{entity} saved or updated.";
            return value;
        }
        #endregion
        #region NotAdded
        public static string NotAdded(string entity)
        {
            var value = $"{entity} not added";
            return value;
        }
        #endregion
        #region NotValid
        public static string NotValid(string entity)
        {
            var value = $"{entity} is invalid";
            return value;
        }
        #endregion
        #region NotSavedOrUpdated
        public static string NotSavedOrUpdated(string entity)
        {
            var value = $"{entity} not saved or updated.";
            return value;
        }
        #endregion
        #region OneAddedToOne
        public static string OneAddedToOne(string entity, string toEntity)
        {
            var value = $"{entity} added to the {toEntity}.";
            return value;
        }
        #endregion
        #region AddOneToOneError
        public static string AddOneToOneError(string entity, string toEntity)
        {
            var value = $"An error occured while adding a {entity} to the {toEntity}!";
            return value;
        }
        #endregion
        #region OneNotAddedToOne
        public static string OneNotAddedToOne(string entity, string toEntity)
        {
            var value = $"{entity} not added to the {toEntity}!";
            return value;
        }
        #endregion
        #region AlreadyExists
        public static string AlreadyExists(string entity)
        {
            var value = $"{entity} already exists";
            return value;
        }
        #endregion
        #region NotExists
        public static string NotExists(string entity)
        {
            var value = $"{entity} doesn't exist";
            return value;
        }
        #endregion
        #region OneOrOneNotExists
        public static string OneOrOneNotExists(string entity, string andEntity)
        {
            var value = $"{entity} or {andEntity} doesn't exist!";
            return value;
        }
        #endregion
        #region Updated
        public static string Updated(string entity)
        {
            var value = $"{entity} has been updated.";
            return value;
        }
        #endregion
        #region NotUpdated
        public static string NotUpdated(string entity)
        {
            var value = $"{entity} not updated.";
            return value;
        }
        #endregion
        #region UpdateError
        public static string UpdateError(string entity)
        {
            var value = $"An error occured while updating the {entity}!";
            return value;
        }
        #endregion
        #region Deleted
        public static string Deleted(string entity)
        {
            var value = $"{entity} has been deleted.";
            return value;
        }
        #endregion
        #region DeleteError
        public static string DeleteError(string entity)
        {
            var value = $"An error occured while deleting the {entity}!";
            return value;
        }
        #endregion
        #region NotDeleted
        public static string NotDeleted(string entity)
        {
            var value = $"{entity} not deleted!";
            return value;
        }
        #endregion
        #region GetError
        public static string GetError(string entity)
        {
            var value = $"Cannot fetch the {entity}!";
            return value;
        }
        #endregion
        #region GetSuccessful
        public static string GetSuccessful(string entity)
        {
            var value = $"{entity} fetched successfully";
            return value;
        }
        #endregion
        #region NotFound
        public static string NotFound(string entity)
        {
            var value = $"{entity} not found!";
            return value;
        }
        #endregion
        #region Found
        public static string Found(string entity)
        {
            var value = $"{entity} found.";
            return value;
        }
        #endregion
        #region NameIsSame
        public static string NameIsSame(string entity)
        {
            var value = $"You entered your previous {entity}.";
            return value;
        }
        #endregion
        #region OneInOne
        public static string OneInOne(string inOne, string one)
        {
            var value = $"{inOne} contains this {one}.";
            return value;
        }
        #endregion
        #region OneNotInOne
        public static string OneNotInOne(string inOne, string one)
        {
            var value = $"{inOne} doesn't contain this {one}!";
            return value;
        }
        #endregion
        #region AreNotEqual
        public static string AreNotEqual(string ones)
        {
            var value = $"{ones} are not equal!";
            return value;
        }
        #endregion
        #region Error
        public static string Error(string entity)
        {
            var value = $"{entity} error !";
            return value;
        }
        #endregion
        #region OneHasNoOne
        public static string OneHasNoOne(string one, string hasOne)
        {
            var value = $"{one} doesn't have any {hasOne}!";
            return value;
        }
        #endregion
        #region Overloading
        public static string Overloading(string model)
        {
            var value = $"{model} overloading ! ";
            return value;
        }
        #endregion
        #region LoginSuccessful
        public static string LoginSuccessful() => "Login successful";
        #endregion
        #region LoginFailed
        public static string LoginFailed() => "Login failed";
        #endregion
        #region LogoutSuccessful
        public static string LogoutSuccessful() => "Successfully signed out";
        #endregion
        #region LogoutFailed
        public static string LogoutFailed() => "Failed while signing out";
        #endregion
        #region RegisterSuccessful
        public static string RegisterSuccessful() => "Successfully registered";
        #endregion
        #region RegisterFailed
        public static string RegisterFailed() => "Register failed";
        #endregion
        #region PasswordNotCorrect
        public static string PasswordNotCorrect() => "Password not correct";
        #endregion
        #region AccountNotActivated
        public static string AccountNotActivated() => "Your code is true but an error occured while activating your account";
        #endregion
        #region AccountActivated
        public static string AccountActivated() => "Your account is activated";
        #endregion
        #region RefreshTokenNotCreated
        public static string RefreshTokenNotCreated() => "Refresh token not created";
        #endregion
        #region RefreshTokenNotValid
        public static string RefreshTokenNotValid() => "Refresh token not valid";
        #endregion

        #region ErrorNotEmpty
        public static string ErrorNotEmpty(string entity)
        {
            var value = $"{entity} cannot be empty";
            return value;
        }
        #endregion
        #region ErrorNotNull
        public static string ErrorNotNull(string entity)
        {
            var value = $"{entity} cannot be null";
            return value;
        }
        #endregion
        #region ErrorBetween
        public static string ErrorBetween(string entity,int bottom, int top)
        {
            var value = $"{entity} must be between {bottom} and {top}";
            return value;
        }
        #endregion
        #region ErrorLength
        public static string ErrorLength(string entity, int v)
        {
            var value = $"{entity} must be {v} characters";
            return value;
        }
        #endregion
        #region ErrorBiggerThan
        public static string ErrorBiggerThan(string entity, int v)
        {
            var value = $"{entity} must be bigger than {v}";
            return value;
        }
        #endregion
        #region ErrorEqualOrBiggerThan
        public static string ErrorEqualOrBiggerThan(string entity, int v)
        {
            var value = $"{entity} must be equal or bigger than {v}";
            return value;
        }
        #endregion
        #region ErrorMaxLength
        public static string ErrorMaxLength(string entity, int v)
        {
            var value = $"{entity} can have Maximum {v} charatecters";
            return value;
        }
        #endregion

    }
}
