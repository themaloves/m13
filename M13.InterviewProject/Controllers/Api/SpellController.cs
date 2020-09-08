using System.Collections.Generic;
using System.Threading.Tasks;
using M13.Domain.Constants;
using M13.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Controllers.Api
{
    public class SpellController : BaseApiController
    {
        #region Private fields

        private readonly ISpellService _spellService;

        #endregion

        #region Ctor

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }

        #endregion
        
        /// <summary>
        /// Получить ошибки по домену
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet(ApiRouteConstants.DefaultApiAction + "/{page}")]
        public async Task<ActionResult<ICollection<string>>> Errors(string page)
        {
            var result = await _spellService.GetErrorsByPageAsync(page, _cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Получить кол-во ошибок по домену
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet(ApiRouteConstants.DefaultApiAction + "/{page}")]
        public async Task<ActionResult<int>> ErrorsCount(string page)
        {
            var result = (await _spellService.GetErrorsByPageAsync(page, _cancellationToken)).Count;

            return Ok(result);
        }
    }
}