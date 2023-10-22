#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Networking.Protocol {

  /// <summary>Holder for reflection information generated from routeguide.proto</summary>
  public static partial class RouteguideReflection {

    #region Descriptor
    /// <summary>File descriptor for routeguide.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RouteguideReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChByb3V0ZWd1aWRlLnByb3RvEhNuZXR3b3JraW5nLnByb3RvY29sIioKBFVz",
            "ZXISEAoIdXNlcm5hbWUYASABKAkSEAoIcGFzc3dvcmQYAiABKAkidgoIRmVz",
            "dGl2YWwSCgoCaWQYASABKAUSEgoKYXJ0aXN0TmFtZRgCIAEoCRIQCghkYXRl",
            "VGltZRgDIAEoCRINCgVwbGFjZRgEIAEoCRIWCg5hdmFpbGFibGVTcG90cxgF",
            "IAEoBRIRCglzb2xkU3BvdHMYBiABKAUibwoGVGlja2V0EgoKAmlkGAEgASgF",
            "EhEKCWJ1eWVyTmFtZRgCIAEoCRIvCghmZXN0aXZhbBgDIAEoCzIdLm5ldHdv",
            "cmtpbmcucHJvdG9jb2wuRmVzdGl2YWwSFQoNbnVtYmVyT2ZTcG90cxgEIAEo",
            "BSK8AgoHUmVxdWVzdBIvCgR0eXBlGAEgASgOMiEubmV0d29ya2luZy5wcm90",
            "b2NvbC5SZXF1ZXN0LlR5cGUSKQoEdXNlchgCIAEoCzIZLm5ldHdvcmtpbmcu",
            "cHJvdG9jb2wuVXNlckgAEjEKCGZlc3RpdmFsGAMgASgLMh0ubmV0d29ya2lu",
            "Zy5wcm90b2NvbC5GZXN0aXZhbEgAEi0KBnRpY2tldBgEIAEoCzIbLm5ldHdv",
            "cmtpbmcucHJvdG9jb2wuVGlja2V0SAAiaAoEVHlwZRILCgdVbmtub3duEAAS",
            "CQoFTE9HSU4QARIKCgZMT0dPVVQQAhIRCg1HRVRfRkVTVElWQUxTEAMSGQoV",
            "R0VUX0ZFU1RJVkFMU19PTl9EQVRFEAQSDgoKQlVZX1RJQ0tFVBAFQgkKB3Bh",
            "eWxvYWQikwIKCFJlc3BvbnNlEjAKBHR5cGUYASABKA4yIi5uZXR3b3JraW5n",
            "LnByb3RvY29sLlJlc3BvbnNlLlR5cGUSDQoFZXJyb3IYAiABKAkSMAoJZmVz",
            "dGl2YWxzGAMgAygLMh0ubmV0d29ya2luZy5wcm90b2NvbC5GZXN0aXZhbBIr",
            "CgZ0aWNrZXQYBCABKAsyGy5uZXR3b3JraW5nLnByb3RvY29sLlRpY2tldCJn",
            "CgRUeXBlEgsKB1Vua25vd24QABIGCgJPSxABEgkKBUVSUk9SEAISEQoNR0VU",
            "X0ZFU1RJVkFMUxADEhkKFUdFVF9GRVNUSVZBTFNfT05fREFURRAEEhEKDVRJ",
            "Q0tFVF9CT1VHSFQQBUItChluZXR3b3JrLnByb3RvYnVmZnByb3RvY29sQhBU",
            "aWNrZXRQcm90b2J1ZmZzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Networking.Protocol.User), global::Networking.Protocol.User.Parser, new[]{ "Username", "Password" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Networking.Protocol.Festival), global::Networking.Protocol.Festival.Parser, new[]{ "Id", "ArtistName", "DateTime", "Place", "AvailableSpots", "SoldSpots" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Networking.Protocol.Ticket), global::Networking.Protocol.Ticket.Parser, new[]{ "Id", "BuyerName", "Festival", "NumberOfSpots" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Networking.Protocol.Request), global::Networking.Protocol.Request.Parser, new[]{ "Type", "User", "Festival", "Ticket" }, new[]{ "Payload" }, new[]{ typeof(global::Networking.Protocol.Request.Types.Type) }, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Networking.Protocol.Response), global::Networking.Protocol.Response.Parser, new[]{ "Type", "Error", "Festivals", "Ticket" }, null, new[]{ typeof(global::Networking.Protocol.Response.Types.Type) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class User : pb::IMessage<User> {
    private static readonly pb::MessageParser<User> _parser = new pb::MessageParser<User>(() => new User());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<User> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Networking.Protocol.RouteguideReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public User() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public User(User other) : this() {
      username_ = other.username_;
      password_ = other.password_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public User Clone() {
      return new User(this);
    }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as User);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(User other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Username != other.Username) return false;
      if (Password != other.Password) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(User other) {
      if (other == null) {
        return;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Username = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Festival : pb::IMessage<Festival> {
    private static readonly pb::MessageParser<Festival> _parser = new pb::MessageParser<Festival>(() => new Festival());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Festival> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Networking.Protocol.RouteguideReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Festival() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Festival(Festival other) : this() {
      id_ = other.id_;
      artistName_ = other.artistName_;
      dateTime_ = other.dateTime_;
      place_ = other.place_;
      availableSpots_ = other.availableSpots_;
      soldSpots_ = other.soldSpots_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Festival Clone() {
      return new Festival(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "artistName" field.</summary>
    public const int ArtistNameFieldNumber = 2;
    private string artistName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ArtistName {
      get { return artistName_; }
      set {
        artistName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "dateTime" field.</summary>
    public const int DateTimeFieldNumber = 3;
    private string dateTime_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string DateTime {
      get { return dateTime_; }
      set {
        dateTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "place" field.</summary>
    public const int PlaceFieldNumber = 4;
    private string place_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Place {
      get { return place_; }
      set {
        place_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "availableSpots" field.</summary>
    public const int AvailableSpotsFieldNumber = 5;
    private int availableSpots_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AvailableSpots {
      get { return availableSpots_; }
      set {
        availableSpots_ = value;
      }
    }

    /// <summary>Field number for the "soldSpots" field.</summary>
    public const int SoldSpotsFieldNumber = 6;
    private int soldSpots_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SoldSpots {
      get { return soldSpots_; }
      set {
        soldSpots_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Festival);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Festival other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (ArtistName != other.ArtistName) return false;
      if (DateTime != other.DateTime) return false;
      if (Place != other.Place) return false;
      if (AvailableSpots != other.AvailableSpots) return false;
      if (SoldSpots != other.SoldSpots) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (ArtistName.Length != 0) hash ^= ArtistName.GetHashCode();
      if (DateTime.Length != 0) hash ^= DateTime.GetHashCode();
      if (Place.Length != 0) hash ^= Place.GetHashCode();
      if (AvailableSpots != 0) hash ^= AvailableSpots.GetHashCode();
      if (SoldSpots != 0) hash ^= SoldSpots.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (ArtistName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ArtistName);
      }
      if (DateTime.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(DateTime);
      }
      if (Place.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Place);
      }
      if (AvailableSpots != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(AvailableSpots);
      }
      if (SoldSpots != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(SoldSpots);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (ArtistName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ArtistName);
      }
      if (DateTime.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(DateTime);
      }
      if (Place.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Place);
      }
      if (AvailableSpots != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(AvailableSpots);
      }
      if (SoldSpots != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SoldSpots);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Festival other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.ArtistName.Length != 0) {
        ArtistName = other.ArtistName;
      }
      if (other.DateTime.Length != 0) {
        DateTime = other.DateTime;
      }
      if (other.Place.Length != 0) {
        Place = other.Place;
      }
      if (other.AvailableSpots != 0) {
        AvailableSpots = other.AvailableSpots;
      }
      if (other.SoldSpots != 0) {
        SoldSpots = other.SoldSpots;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            ArtistName = input.ReadString();
            break;
          }
          case 26: {
            DateTime = input.ReadString();
            break;
          }
          case 34: {
            Place = input.ReadString();
            break;
          }
          case 40: {
            AvailableSpots = input.ReadInt32();
            break;
          }
          case 48: {
            SoldSpots = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Ticket : pb::IMessage<Ticket> {
    private static readonly pb::MessageParser<Ticket> _parser = new pb::MessageParser<Ticket>(() => new Ticket());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Ticket> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Networking.Protocol.RouteguideReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Ticket() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Ticket(Ticket other) : this() {
      id_ = other.id_;
      buyerName_ = other.buyerName_;
      Festival = other.festival_ != null ? other.Festival.Clone() : null;
      numberOfSpots_ = other.numberOfSpots_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Ticket Clone() {
      return new Ticket(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "buyerName" field.</summary>
    public const int BuyerNameFieldNumber = 2;
    private string buyerName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string BuyerName {
      get { return buyerName_; }
      set {
        buyerName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "festival" field.</summary>
    public const int FestivalFieldNumber = 3;
    private global::Networking.Protocol.Festival festival_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Festival Festival {
      get { return festival_; }
      set {
        festival_ = value;
      }
    }

    /// <summary>Field number for the "numberOfSpots" field.</summary>
    public const int NumberOfSpotsFieldNumber = 4;
    private int numberOfSpots_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int NumberOfSpots {
      get { return numberOfSpots_; }
      set {
        numberOfSpots_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Ticket);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Ticket other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (BuyerName != other.BuyerName) return false;
      if (!object.Equals(Festival, other.Festival)) return false;
      if (NumberOfSpots != other.NumberOfSpots) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (BuyerName.Length != 0) hash ^= BuyerName.GetHashCode();
      if (festival_ != null) hash ^= Festival.GetHashCode();
      if (NumberOfSpots != 0) hash ^= NumberOfSpots.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (BuyerName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(BuyerName);
      }
      if (festival_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Festival);
      }
      if (NumberOfSpots != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(NumberOfSpots);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (BuyerName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BuyerName);
      }
      if (festival_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Festival);
      }
      if (NumberOfSpots != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(NumberOfSpots);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Ticket other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.BuyerName.Length != 0) {
        BuyerName = other.BuyerName;
      }
      if (other.festival_ != null) {
        if (festival_ == null) {
          festival_ = new global::Networking.Protocol.Festival();
        }
        Festival.MergeFrom(other.Festival);
      }
      if (other.NumberOfSpots != 0) {
        NumberOfSpots = other.NumberOfSpots;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            BuyerName = input.ReadString();
            break;
          }
          case 26: {
            if (festival_ == null) {
              festival_ = new global::Networking.Protocol.Festival();
            }
            input.ReadMessage(festival_);
            break;
          }
          case 32: {
            NumberOfSpots = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Request : pb::IMessage<Request> {
    private static readonly pb::MessageParser<Request> _parser = new pb::MessageParser<Request>(() => new Request());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Request> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Networking.Protocol.RouteguideReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Request() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Request(Request other) : this() {
      type_ = other.type_;
      switch (other.PayloadCase) {
        case PayloadOneofCase.User:
          User = other.User.Clone();
          break;
        case PayloadOneofCase.Festival:
          Festival = other.Festival.Clone();
          break;
        case PayloadOneofCase.Ticket:
          Ticket = other.Ticket.Clone();
          break;
      }

    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Request Clone() {
      return new Request(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Networking.Protocol.Request.Types.Type type_ = 0;
    /// <summary>
    /// Identifies which request is filled in.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Request.Types.Type Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "user" field.</summary>
    public const int UserFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.User User {
      get { return payloadCase_ == PayloadOneofCase.User ? (global::Networking.Protocol.User) payload_ : null; }
      set {
        payload_ = value;
        payloadCase_ = value == null ? PayloadOneofCase.None : PayloadOneofCase.User;
      }
    }

    /// <summary>Field number for the "festival" field.</summary>
    public const int FestivalFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Festival Festival {
      get { return payloadCase_ == PayloadOneofCase.Festival ? (global::Networking.Protocol.Festival) payload_ : null; }
      set {
        payload_ = value;
        payloadCase_ = value == null ? PayloadOneofCase.None : PayloadOneofCase.Festival;
      }
    }

    /// <summary>Field number for the "ticket" field.</summary>
    public const int TicketFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Ticket Ticket {
      get { return payloadCase_ == PayloadOneofCase.Ticket ? (global::Networking.Protocol.Ticket) payload_ : null; }
      set {
        payload_ = value;
        payloadCase_ = value == null ? PayloadOneofCase.None : PayloadOneofCase.Ticket;
      }
    }

    private object payload_;
    /// <summary>Enum of possible cases for the "payload" oneof.</summary>
    public enum PayloadOneofCase {
      None = 0,
      User = 2,
      Festival = 3,
      Ticket = 4,
    }
    private PayloadOneofCase payloadCase_ = PayloadOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PayloadOneofCase PayloadCase {
      get { return payloadCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearPayload() {
      payloadCase_ = PayloadOneofCase.None;
      payload_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Request);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Request other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (!object.Equals(User, other.User)) return false;
      if (!object.Equals(Festival, other.Festival)) return false;
      if (!object.Equals(Ticket, other.Ticket)) return false;
      if (PayloadCase != other.PayloadCase) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (payloadCase_ == PayloadOneofCase.User) hash ^= User.GetHashCode();
      if (payloadCase_ == PayloadOneofCase.Festival) hash ^= Festival.GetHashCode();
      if (payloadCase_ == PayloadOneofCase.Ticket) hash ^= Ticket.GetHashCode();
      hash ^= (int) payloadCase_;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Type != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (payloadCase_ == PayloadOneofCase.User) {
        output.WriteRawTag(18);
        output.WriteMessage(User);
      }
      if (payloadCase_ == PayloadOneofCase.Festival) {
        output.WriteRawTag(26);
        output.WriteMessage(Festival);
      }
      if (payloadCase_ == PayloadOneofCase.Ticket) {
        output.WriteRawTag(34);
        output.WriteMessage(Ticket);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (payloadCase_ == PayloadOneofCase.User) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(User);
      }
      if (payloadCase_ == PayloadOneofCase.Festival) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Festival);
      }
      if (payloadCase_ == PayloadOneofCase.Ticket) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Ticket);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Request other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      switch (other.PayloadCase) {
        case PayloadOneofCase.User:
          User = other.User;
          break;
        case PayloadOneofCase.Festival:
          Festival = other.Festival;
          break;
        case PayloadOneofCase.Ticket:
          Ticket = other.Ticket;
          break;
      }

    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            type_ = (global::Networking.Protocol.Request.Types.Type) input.ReadEnum();
            break;
          }
          case 18: {
            global::Networking.Protocol.User subBuilder = new global::Networking.Protocol.User();
            if (payloadCase_ == PayloadOneofCase.User) {
              subBuilder.MergeFrom(User);
            }
            input.ReadMessage(subBuilder);
            User = subBuilder;
            break;
          }
          case 26: {
            global::Networking.Protocol.Festival subBuilder = new global::Networking.Protocol.Festival();
            if (payloadCase_ == PayloadOneofCase.Festival) {
              subBuilder.MergeFrom(Festival);
            }
            input.ReadMessage(subBuilder);
            Festival = subBuilder;
            break;
          }
          case 34: {
            global::Networking.Protocol.Ticket subBuilder = new global::Networking.Protocol.Ticket();
            if (payloadCase_ == PayloadOneofCase.Ticket) {
              subBuilder.MergeFrom(Ticket);
            }
            input.ReadMessage(subBuilder);
            Ticket = subBuilder;
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Request message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Type {
        [pbr::OriginalName("Unknown")] Unknown = 0,
        [pbr::OriginalName("LOGIN")] Login = 1,
        [pbr::OriginalName("LOGOUT")] Logout = 2,
        [pbr::OriginalName("GET_FESTIVALS")] GetFestivals = 3,
        [pbr::OriginalName("GET_FESTIVALS_ON_DATE")] GetFestivalsOnDate = 4,
        [pbr::OriginalName("BUY_TICKET")] BuyTicket = 5,
      }

    }
    #endregion

  }

  public sealed partial class Response : pb::IMessage<Response> {
    private static readonly pb::MessageParser<Response> _parser = new pb::MessageParser<Response>(() => new Response());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Networking.Protocol.RouteguideReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response(Response other) : this() {
      type_ = other.type_;
      error_ = other.error_;
      festivals_ = other.festivals_.Clone();
      Ticket = other.ticket_ != null ? other.Ticket.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Response Clone() {
      return new Response(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Networking.Protocol.Response.Types.Type type_ = 0;
    /// <summary>
    /// Identifies which request is filled in.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Response.Types.Type Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "error" field.</summary>
    public const int ErrorFieldNumber = 2;
    private string error_ = "";
    /// <summary>
    /// One of the following will be filled in, depending on the type.
    /// One of the following will be filled in, depending on the type.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Error {
      get { return error_; }
      set {
        error_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "festivals" field.</summary>
    public const int FestivalsFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Networking.Protocol.Festival> _repeated_festivals_codec
        = pb::FieldCodec.ForMessage(26, global::Networking.Protocol.Festival.Parser);
    private readonly pbc::RepeatedField<global::Networking.Protocol.Festival> festivals_ = new pbc::RepeatedField<global::Networking.Protocol.Festival>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Networking.Protocol.Festival> Festivals {
      get { return festivals_; }
    }

    /// <summary>Field number for the "ticket" field.</summary>
    public const int TicketFieldNumber = 4;
    private global::Networking.Protocol.Ticket ticket_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Networking.Protocol.Ticket Ticket {
      get { return ticket_; }
      set {
        ticket_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (Error != other.Error) return false;
      if(!festivals_.Equals(other.festivals_)) return false;
      if (!object.Equals(Ticket, other.Ticket)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (Error.Length != 0) hash ^= Error.GetHashCode();
      hash ^= festivals_.GetHashCode();
      if (ticket_ != null) hash ^= Ticket.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Type != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (Error.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Error);
      }
      festivals_.WriteTo(output, _repeated_festivals_codec);
      if (ticket_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Ticket);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (Error.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Error);
      }
      size += festivals_.CalculateSize(_repeated_festivals_codec);
      if (ticket_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Ticket);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Response other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.Error.Length != 0) {
        Error = other.Error;
      }
      festivals_.Add(other.festivals_);
      if (other.ticket_ != null) {
        if (ticket_ == null) {
          ticket_ = new global::Networking.Protocol.Ticket();
        }
        Ticket.MergeFrom(other.Ticket);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            type_ = (global::Networking.Protocol.Response.Types.Type) input.ReadEnum();
            break;
          }
          case 18: {
            Error = input.ReadString();
            break;
          }
          case 26: {
            festivals_.AddEntriesFrom(input, _repeated_festivals_codec);
            break;
          }
          case 34: {
            if (ticket_ == null) {
              ticket_ = new global::Networking.Protocol.Ticket();
            }
            input.ReadMessage(ticket_);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Response message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Type {
        [pbr::OriginalName("Unknown")] Unknown = 0,
        [pbr::OriginalName("OK")] Ok = 1,
        [pbr::OriginalName("ERROR")] Error = 2,
        [pbr::OriginalName("GET_FESTIVALS")] GetFestivals = 3,
        [pbr::OriginalName("GET_FESTIVALS_ON_DATE")] GetFestivalsOnDate = 4,
        [pbr::OriginalName("TICKET_BOUGHT")] TicketBought = 5,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code

