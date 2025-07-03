using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

using Appwrite;
using Appwrite.Models;
using Appwrite.Enums;
using Appwrite.Services;
using NUnit.Framework;
using Console = System.Console;
namespace AppwriteTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Test Started");
        }

        [Test]
        public IEnumerator Test1()
        {
            var task = Test1Async();
            yield return new WaitUntil(() => task.IsCompleted);
        }

        public async Task Test1Async()
        {
            var client = new Client()
                .AddHeader("Origin", "http://localhost")
                .SetSelfSigned(true);

            var foo = new Foo(client);
            var bar = new Bar(client);
            var general = new General(client);

            Mock mock;
            // Foo Tests
            mock = await foo.Get("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await foo.Post("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await foo.Put("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await foo.Patch("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await foo.Delete("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            // Bar Tests
            mock = await bar.Get("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await bar.Post("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await bar.Put("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await bar.Patch("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            mock = await bar.Delete("string", 123, new List<string>() { "string in array" });
            Console.WriteLine(mock.Result);

            // General Tests
            var result = await general.Redirect();
            Console.WriteLine((result as Dictionary<string, object>)["result"]);

            mock = await general.Upload("string", 123, new List<string>() { "string in array" }, InputFile.FromPath("../../../../../../resources/file.png"));
            Console.WriteLine(mock.Result);

            mock = await general.Upload("string", 123, new List<string>() { "string in array" }, InputFile.FromPath("../../../../../../resources/large_file.mp4"));
            Console.WriteLine(mock.Result);

            var info = new FileInfo("../../../../../../resources/file.png");
            mock = await general.Upload("string", 123, new List<string>() { "string in array" }, InputFile.FromStream(info.OpenRead(), "file.png", "image/png"));
            Console.WriteLine(mock.Result);

            info = new FileInfo("../../../../../../resources/large_file.mp4");
            mock = await general.Upload("string", 123, new List<string>() { "string in array" }, InputFile.FromStream(info.OpenRead(), "large_file.mp4", "video/mp4"));
            Console.WriteLine(mock.Result);

            mock = await general.Enum(MockType.First);
            Console.WriteLine(mock.Result);

            try
            {
                await general.Error400();
            }
            catch (AppwriteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Response);
            }

            try
            {
                await general.Error500();
            }
            catch (AppwriteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Response);
            }

            try
            {
                await general.Error502();
            }
            catch (AppwriteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Response);
            }

            try
            {
                client.SetEndpoint("htp://cloud.appwrite.io/v1");
            }
            catch (AppwriteException e)
            {
                Console.WriteLine(e.Message);
            }

            await general.Empty();

            var url = await general.Oauth2(
                clientId: "clientId",
                scopes: new List<string>() {"test"},
                state: "123456",
                success: "https://localhost",
                failure: "https://localhost"
            );
            Console.WriteLine(url);

            // Query helper tests
            Console.WriteLine(Query.Equal("released", new List<bool> { true }));
            Console.WriteLine(Query.Equal("title", new List<string> { "Spiderman", "Dr. Strange" }));
            Console.WriteLine(Query.NotEqual("title", "Spiderman"));
            Console.WriteLine(Query.LessThan("releasedYear", 1990));
            Console.WriteLine(Query.GreaterThan("releasedYear", 1990));
            Console.WriteLine(Query.Search("name", "john"));
            Console.WriteLine(Query.IsNull("name"));
            Console.WriteLine(Query.IsNotNull("name"));
            Console.WriteLine(Query.Between("age", 50, 100));
            Console.WriteLine(Query.Between("age", 50.5, 100.5));
            Console.WriteLine(Query.Between("name", "Anna", "Brad"));
            Console.WriteLine(Query.StartsWith("name", "Ann"));
            Console.WriteLine(Query.EndsWith("name", "nne"));
            Console.WriteLine(Query.Select(new List<string> { "name", "age" }));
            Console.WriteLine(Query.OrderAsc("title"));
            Console.WriteLine(Query.OrderDesc("title"));
            Console.WriteLine(Query.CursorAfter("my_movie_id"));
            Console.WriteLine(Query.CursorBefore("my_movie_id"));
            Console.WriteLine(Query.Limit(50));
            Console.WriteLine(Query.Offset(20));
            Console.WriteLine(Query.Contains("title", "Spider"));
            Console.WriteLine(Query.Contains("labels", "first"));
            Console.WriteLine(Query.Or(
                new List<string> {
                    Query.Equal("released", true),
                    Query.LessThan("releasedYear", 1990)
                }
            ));
            Console.WriteLine(Query.And(
                new List<string> {
                    Query.Equal("released", false),
                    Query.GreaterThan("releasedYear", 2015)
                }
            ));

            // Permission & Roles helper tests
            Console.WriteLine(Permission.Read(Role.Any()));
            Console.WriteLine(Permission.Read(Role.User(ID.Custom("123"))));
            Console.WriteLine(Permission.Read(Role.Users()));
            Console.WriteLine(Permission.Read(Role.Guests()));
            Console.WriteLine(Permission.Read(Role.Team("123")));
            Console.WriteLine(Permission.Read(Role.Team("123", "admin")));
            Console.WriteLine(Permission.Read(Role.Member("123")));
            Console.WriteLine(Permission.Read(Role.Users("verified")));
            Console.WriteLine(Permission.Read(Role.Users("unverified")));
            Console.WriteLine(Permission.Read(Role.Label("admin")));

            // ID helper tests
            Console.WriteLine(ID.Unique());
            Console.WriteLine(ID.Custom("custom_id"));

            mock = await general.Headers();
            Console.WriteLine(mock.Result);
        }
    }
}
