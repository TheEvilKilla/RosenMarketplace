// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Dal.Repositories;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly MarketplaceDb _context;

    #endregion

    #region Constructors

    public UserRepository()
    {
        _context = new MarketplaceDb();
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<User[]> GetAllUsersAsync()
    {
        return await _context.GetUsersAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.GetUserByUsernameAsync(username);
    }
    
    public async Task<User> AddUserAsync(User user)
    {
        return await _context.AddUserAsync(user);
    }

    public async Task<Offer> AddOfferAsync(Offer offer, int userId)
    {
        return await _context.AddOfferAsync(offer, userId);
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        return await _context.AddCategoryAsync(category);
    }

    public async Task<Offer> GetOfferAsync(Guid offerId)
    {
        return await _context.GetOfferAsync(offerId);
    }

    public async Task<Category> GetCategoryAsync(byte id)
    {
        return await _context.GetCategoryAsync(id);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.GetUserByIdAsync(id);
    }

    #endregion
}