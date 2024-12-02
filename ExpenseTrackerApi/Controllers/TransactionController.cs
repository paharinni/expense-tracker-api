using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTrackerApi.Controllers;

[Authorize]
[Route("api/transaction")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
    {
        return await _context.Transactions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransactionByIdAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        return transaction;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransactionAsync(int id, Transaction transaction)
    {
        var transactionToUpdate = await _context.Transactions.FindAsync(id);
        if (transactionToUpdate == null)
        {
            return NotFound();
        }

        transactionToUpdate.Description = transaction.Description;
        transactionToUpdate.Name = transaction.Name;
        transactionToUpdate.Amount = transaction.Amount;
        transactionToUpdate.DateCompleted = transaction.DateCompleted;
        transactionToUpdate.UserId = transaction.UserId;
        transactionToUpdate.CategoryId = transaction.CategoryId;

        _context.Update(transactionToUpdate);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransactionExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransactionAsync(Transaction transaction)
    {
        var createdTransaction = new Transaction()
        {
            Name = transaction.Name,
            Amount = transaction.Amount,
            Description = transaction.Description,
            DateCompleted = transaction.DateCompleted,
            UserId = transaction.UserId,
            CategoryId = transaction.CategoryId,
        };
            
        _context.Transactions.Add(createdTransaction);
        await _context.SaveChangesAsync();

        return Ok(transaction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }
}