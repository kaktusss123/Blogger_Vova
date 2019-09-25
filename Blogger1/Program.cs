using System;
using System.Collections.Generic;

namespace Blogger1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input;
            int id;
            var blog = new Blog();
            do
            {
                Console.WriteLine("\n1. Добавить новый пост");
                Console.WriteLine("2. Отредактировать пост");
                Console.WriteLine("3. Удалить пост");
                Console.WriteLine("4. Вывести пост");
                Console.WriteLine("5. Вывести все посты");
                Console.WriteLine("6. Вывести заголовки постов");
                Console.WriteLine("0. Выход");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        blog.AddPost();
                        break;
                    case "2":
                        Console.Write("ID необходимого поста: ");
                        id = int.Parse(Console.ReadLine());
                        blog.EditPost(id);
                        break;
                    case "3":
                        Console.Write("ID необходимого поста: ");
                        id = int.Parse(Console.ReadLine());
                        blog.RemovePost(id);
                        break;
                    case "4":
                        Console.Write("ID необходимого поста: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(blog.GetPost(id));
                        break;
                    case "5":
                        Console.WriteLine(blog.GetAllPosts());
                        break;
                    case "6":
                        Console.WriteLine(blog.GetTitles());
                        break;
                }
            } while (input != "0");
        }
    }
}

public class Blog
{
    private readonly List<Post> _listOfPosts;
    private readonly Random _rand;

    public Blog()
    {
        _listOfPosts = new List<Post>();
        _rand = new Random();
    }

    public void AddPost()
    {
        Console.Write("Заголовок: ");
        string title = Console.ReadLine();
        Console.Write("Тело: ");
        string body = Console.ReadLine();
        _listOfPosts.Add(new Post(title, body, _rand.Next()));
    }

    public string GetAllPosts()
    {
        var printablePosts = new List<string>();
        foreach (var post in _listOfPosts)
            printablePosts.Add($"{post.GetId()}) {post.GetTitle()} | {post.GetTime()}\n\n{post.GetBody()}");

        return string.Join("\n\n======================\n\n", printablePosts);
    }

    public string GetPost(int id)
    {
        foreach (var post in _listOfPosts)
        {
            if (post.GetId() == id)
            {
                return $"{post.GetId()}) {post.GetTitle()} | {post.GetTime()}\n\n{post.GetBody()}\n";
            }
        }

        return "Пост не найден!";
    }

    public void RemovePost(int id)
    {
        var found = false;
        foreach (var post in _listOfPosts)
        {
            if (post.GetId() != id) continue;
            found = true;
            _listOfPosts.Remove(post);
        }

        if (!found)
            Console.WriteLine("Пост не найден");
    }

    public void EditPost(int id)
    {
        var found = false;
        foreach (var post in _listOfPosts)
        {
            if (post.GetId() != id) continue;
            found = true;
            Console.Write("Новый заголовок(оставьте пустым, чтобы не изменять): ");
            var newTitle = Console.ReadLine();
            if (newTitle != "")
                post.SetTitle(newTitle);

            Console.WriteLine("Новое тело(оставьте пустым, чтобы не изменять): ");
            var newBody = Console.ReadLine();
            if (newBody != "")
                post.SetBody(newBody);
        }

        if (!found)
            Console.WriteLine("Пост не найден!");
    }

    public string GetTitles()
    {
        var titles = new List<string>();
        foreach (var post in _listOfPosts)
            titles.Add(String.Join(" | ", post.GetId(), post.GetTitle(), post.GetTime()));

        return string.Join("\n", titles);
    }
}

public class Post
{
    private string _title;
    private string _body;
    private readonly DateTime _created;
    private readonly int _id;

    public Post(string title, string body, int id)
    {
        _title = title;
        _body = body;
        _created = DateTime.Now;
        _id = id;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetBody()
    {
        return _body;
    }

    public void SetBody(string body)
    {
        _body = body;
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public int GetId()
    {
        return _id;
    }

    public DateTime GetTime()
    {
        return _created;
    }
}