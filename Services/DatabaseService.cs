using SQLite;
using NoteApp.Models;

namespace NoteApp.Services;

public class DatabaseService
{
    private SQLiteConnection _db;

    private void Init()
    {
        if (_db != null) return;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");
        _db = new SQLiteConnection(dbPath);
        _db.CreateTable<Note>();
    }

    public List<Note> GetNotes()
    {
        Init();
        return _db.Table<Note>().ToList();
    }

    public void AddNote(Note note)
    {
        Init();
        _db.Insert(note);
    }

    public void DeleteNote(Note note)
    {
        Init();
        _db.Delete(note);
    }
}