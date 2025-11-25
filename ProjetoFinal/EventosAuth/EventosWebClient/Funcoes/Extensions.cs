using EventosShared.Model;
using System.Text.Json;

namespace EventosWebClient.Funcoes;

public static class Extensions
{
	public static string ToJson(this object obj)
	{
		return JsonSerializer.Serialize(obj);
	}

	public static T FromJson<T>(this string json)
	{
		return JsonSerializer.Deserialize<T>(json)!;
	}

	public static bool HasContent(this ISession session, string key)
	{
		return session.HasContent(key);
	}

	public static T? GetSession<T>(this ISession session, string key)
	{
		var value = session.GetString(key);
		return value is null ? default : value.FromJson<T>();
	}

	public static void SetSession<T>(this ISession session, string key, T value)
	{
		session.SetString(key, value!.ToJson());
	}

	public static void RemoveSession(this ISession session, string key)
	{
		session.Remove(key);
	}

	public static User? GetUserSession(this ISession session)
	{
		return session.GetSession<User>("User");
	}

	public static void SetUserSession(this ISession session, User user)
	{
		session.SetSession("User", user);
	}

	public static void RemoveUserSession(this ISession session)
	{
		session.RemoveSession("User");
	}
}
