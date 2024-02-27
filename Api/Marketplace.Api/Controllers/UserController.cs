// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

namespace Marketplace.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Marketplace.Core.Bl;
    using Marketplace.Core.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Services for Users
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly ILogger<UserController> logger;

        private readonly IUserBl userBl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userBl">The user business logic.</param>
        public UserController(ILogger<UserController> logger, IUserBl userBl)
        {
            this.logger = logger;
            this.userBl = userBl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            IEnumerable<User> result;

            try
            {
                result = await this.userBl.GetUsersAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUserByUsernameAsync(string username)
        {
            User result;

            try
            {
                result = await this.userBl.GetUserByUsernameAsync(username).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            if (result == null)
            {
                return NotFound();
            }

            return this.Ok(result);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            User result;

            try
            {
                result = await this.userBl.AddUserAsync(user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }

        [HttpPost("AddOffer/{userId}")]
        public async Task<ActionResult<Offer>> Post(int userId, [FromBody] Offer offer)
        {
            Offer result;

            try
            {
                result = await this.userBl.AddOfferAsync(offer, userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)
        {
            Category result;

            try
            {
                result = await this.userBl.AddCategoryAsync(category).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }

        [HttpGet("GetOffer/{offerId}")]
        public async Task<ActionResult<Offer>> GetOffer(Guid offerId)
        {
            Offer result;

            try
            {
                result = await this.userBl.GetOfferAsync(offerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            if (result == null)
            {
                return NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<Category>> GetCategory(byte id)
        {
            Category result;

            try
            {
                result = await this.userBl.GetCategoryAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            if (result == null)
            {
                return NotFound();
            }

            return this.Ok(result);
        }

        #endregion
    }
}