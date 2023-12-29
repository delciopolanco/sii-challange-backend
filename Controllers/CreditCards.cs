using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using addCard_backend.Services;
using addCard_backend.DTOs;
using addCard_backend.Models;
using addCard_backend.Utils.Extentions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace addCard_backend.Controllers
{
    [Route("api/[controller]")]
    public class CreditCardController : Controller
    {
        private readonly CreditCardRepository creditCardRepository;
        private readonly SecurityService securityService;

        private readonly IMapper mapper;

        public CreditCardController(CreditCardRepository _creditCardRepository, SecurityService _securityService, IMapper _mapper)
        {
            securityService = _securityService;
            creditCardRepository = _creditCardRepository;
            mapper = _mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PaginatedListDTO<CreditCardIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<PaginatedListDTO<CreditCardIdDTO>>> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                var query = creditCardRepository.Queryable<CreditCardIdDTO>().OrderBy(c => c.CreditcardHolderName);
                var total = await query.CountAsync();
                var pagedQuery = query.Skip((page - 1) * pageSize).Take(pageSize);
                var creditcards = await pagedQuery.ToListAsync();
                var maskedCreditCards = creditcards.MaskCreditCards();


                var creditcardList = new PaginatedListDTO<CreditCardIdDTO>
                {
                    Total = total,
                    CurrentPage = page,
                    Pages = (total - 1) / pageSize + 1,
                    List = maskedCreditCards.ToList()
                };


                return Ok(creditcardList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreditCardIdDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<CreditCardIdDTO>> Get(int id)
        {
            try
            {
                var creditCard = await creditCardRepository.FirstOrDefault<CreditCardIdDTO>(e => e.Id == id);

                if (creditCard == null)
                {
                    return NotFound();
                }

                creditCard.MaskCreditCard();

                return Ok(creditCard);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreditCardIdDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<CreditCardIdDTO>> Post([FromBody] CreditCardDTO creditCardDTO)
        {
            try
            {
                var creditCard = mapper.Map<CreditCard>(creditCardDTO);

                try
                {
                    creditCard.CreditcardNumber = securityService.Encrypt(creditCard.CreditcardNumber);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

                await creditCardRepository.Insert(creditCard);

                var  newCard = mapper.Map<CreditCardIdDTO>(creditCard);
                newCard.MaskCreditCard();

                return Created("", newCard);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult> Put(CreditCardIdDTO creditCardDTO)
        {
            try
            {
                var creditCard = mapper.Map<CreditCard>(creditCardDTO);

                if (string.IsNullOrEmpty(creditCard.Id.ToString())) return new BadRequestResult();

                try
                {
                    creditCard.CreditcardNumber = securityService.Encrypt(creditCard.CreditcardNumber);
                    await creditCardRepository.Update(creditCard);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                if (string.IsNullOrEmpty(id.ToString())) return new BadRequestResult();

                var creditCard = creditCardRepository.Queryable().Where(e => e.Id == id).FirstOrDefault();

                if (creditCard == null) return new BadRequestResult();

                await creditCardRepository.Queryable().Where(e => e.Id == id).ExecuteDeleteAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }

    }
}

