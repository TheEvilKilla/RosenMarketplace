// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Bl;

/// <summary>
///     Users' logic
/// </summary>
/// <seealso cref="Marketplace.Core.Bl.IUserBl" />
public class UserBl : IUserBl
{
    #region Fields

    private readonly IUserRepository userRepository;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserBl" /> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    public UserBl(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await userRepository.GetAllUsersAsync().ConfigureAwait(false);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await userRepository.GetUserByUsernameAsync(username).ConfigureAwait(false);
    }

    public async Task<User> AddUserAsync(User user)
    {
        return await userRepository.AddUserAsync(user).ConfigureAwait(false);
    }

    public async Task<Offer> AddOfferAsync(Offer offer, int userId)
    {
        return await userRepository.AddOfferAsync(offer, userId).ConfigureAwait(false);
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        return await userRepository.AddCategoryAsync(category).ConfigureAwait(false);
    }

    public async Task<Offer> GetOfferAsync(Guid offerId)
    {
        return await userRepository.GetOfferAsync(offerId).ConfigureAwait(false);
    }

    public async Task<Category> GetCategoryAsync(byte id)
    {
        return await userRepository.GetCategoryAsync(id).ConfigureAwait(false);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await userRepository.GetUserByIdAsync(id).ConfigureAwait(false);
    }

    #endregion
}