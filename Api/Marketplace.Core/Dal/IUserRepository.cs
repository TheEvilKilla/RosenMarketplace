// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Marketplace.Core.Model;

namespace Marketplace.Core.Dal;

/// <summary>
///     Contract for the User data access
/// </summary>
public interface IUserRepository
{
    #region Methods

    /// <summary>
    ///     Gets all users asynchronous.
    /// </summary>
    /// <returns>Array of users</returns>
    Task<User[]> GetAllUsersAsync();

    Task<User> GetUserByUsernameAsync(string username);

    Task<User> AddUserAsync(User user);

    Task<Offer> AddOfferAsync(Offer offer, int userId);

    Task<Category> AddCategoryAsync(Category category);

    Task<Offer> GetOfferAsync(Guid offerId);

    Task<Category> GetCategoryAsync(byte id);
    
    Task<User> GetUserByIdAsync(int id);

    #endregion
}