// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=17b3b1f090c3ea48.  DO NOT EDIT!
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace messages {
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class Messages {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    internal static pbd::MessageDescriptor internal__static_messages_Request__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::messages.Request, global::messages.Request.Builder> internal__static_messages_Request__FieldAccessorTable;
    internal static pbd::MessageDescriptor internal__static_messages_Response__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::messages.Response, global::messages.Response.Builder> internal__static_messages_Response__FieldAccessorTable;
    #endregion
    #region Descriptor
    public static pbd::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbd::FileDescriptor descriptor;
    
    static Messages() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5tZXNzYWdlcy5wcm90bxIIbWVzc2FnZXMiGQoHUmVxdWVzdBIOCgZ0eXBl", 
            "SWQYASACKAUiKwoIUmVzcG9uc2USDgoGdHlwZUlkGAEgAigFEg8KB21lc3Nh", 
          "Z2UYAyABKAk="));
      pbd::FileDescriptor.InternalDescriptorAssigner assigner = delegate(pbd::FileDescriptor root) {
        descriptor = root;
        internal__static_messages_Request__Descriptor = Descriptor.MessageTypes[0];
        internal__static_messages_Request__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::messages.Request, global::messages.Request.Builder>(internal__static_messages_Request__Descriptor,
                new string[] { "TypeId", });
        internal__static_messages_Response__Descriptor = Descriptor.MessageTypes[1];
        internal__static_messages_Response__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::messages.Response, global::messages.Response.Builder>(internal__static_messages_Response__Descriptor,
                new string[] { "TypeId", "Message", });
        return null;
      };
      pbd::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbd::FileDescriptor[] {
          }, assigner);
    }
    #endregion
    
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Request : pb::GeneratedMessage<Request, Request.Builder> {
    private Request() { }
    private static readonly Request defaultInstance = new Request().MakeReadOnly();
    private static readonly string[] _requestFieldNames = new string[] { "typeId" };
    private static readonly uint[] _requestFieldTags = new uint[] { 8 };
    public static Request DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override Request DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override Request ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::messages.Messages.internal__static_messages_Request__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<Request, Request.Builder> InternalFieldAccessors {
      get { return global::messages.Messages.internal__static_messages_Request__FieldAccessorTable; }
    }
    
    public const int TypeIdFieldNumber = 1;
    private bool hasTypeId;
    private int typeId_;
    public bool HasTypeId {
      get { return hasTypeId; }
    }
    public int TypeId {
      get { return typeId_; }
    }
    
    public override bool IsInitialized {
      get {
        if (!hasTypeId) return false;
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _requestFieldNames;
      if (hasTypeId) {
        output.WriteInt32(1, field_names[0], TypeId);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasTypeId) {
          size += pb::CodedOutputStream.ComputeInt32Size(1, TypeId);
        }
        size += UnknownFields.SerializedSize;
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    public static Request ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Request ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Request ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Request ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Request ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Request ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static Request ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static Request ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static Request ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Request ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private Request MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(Request prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<Request, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(Request cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private Request result;
      
      private Request PrepareBuilder() {
        if (resultIsReadOnly) {
          Request original = result;
          result = new Request();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override Request MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::messages.Request.Descriptor; }
      }
      
      public override Request DefaultInstanceForType {
        get { return global::messages.Request.DefaultInstance; }
      }
      
      public override Request BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is Request) {
          return MergeFrom((Request) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(Request other) {
        if (other == global::messages.Request.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasTypeId) {
          TypeId = other.TypeId;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_requestFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _requestFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 8: {
              result.hasTypeId = input.ReadInt32(ref result.typeId_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasTypeId {
        get { return result.hasTypeId; }
      }
      public int TypeId {
        get { return result.TypeId; }
        set { SetTypeId(value); }
      }
      public Builder SetTypeId(int value) {
        PrepareBuilder();
        result.hasTypeId = true;
        result.typeId_ = value;
        return this;
      }
      public Builder ClearTypeId() {
        PrepareBuilder();
        result.hasTypeId = false;
        result.typeId_ = 0;
        return this;
      }
    }
    static Request() {
      object.ReferenceEquals(global::messages.Messages.Descriptor, null);
    }
  }
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class Response : pb::GeneratedMessage<Response, Response.Builder> {
    private Response() { }
    private static readonly Response defaultInstance = new Response().MakeReadOnly();
    private static readonly string[] _responseFieldNames = new string[] { "message", "typeId" };
    private static readonly uint[] _responseFieldTags = new uint[] { 26, 8 };
    public static Response DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override Response DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override Response ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::messages.Messages.internal__static_messages_Response__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<Response, Response.Builder> InternalFieldAccessors {
      get { return global::messages.Messages.internal__static_messages_Response__FieldAccessorTable; }
    }
    
    public const int TypeIdFieldNumber = 1;
    private bool hasTypeId;
    private int typeId_;
    public bool HasTypeId {
      get { return hasTypeId; }
    }
    public int TypeId {
      get { return typeId_; }
    }
    
    public const int MessageFieldNumber = 3;
    private bool hasMessage;
    private string message_ = "";
    public bool HasMessage {
      get { return hasMessage; }
    }
    public string Message {
      get { return message_; }
    }
    
    public override bool IsInitialized {
      get {
        if (!hasTypeId) return false;
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _responseFieldNames;
      if (hasTypeId) {
        output.WriteInt32(1, field_names[1], TypeId);
      }
      if (hasMessage) {
        output.WriteString(3, field_names[0], Message);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasTypeId) {
          size += pb::CodedOutputStream.ComputeInt32Size(1, TypeId);
        }
        if (hasMessage) {
          size += pb::CodedOutputStream.ComputeStringSize(3, Message);
        }
        size += UnknownFields.SerializedSize;
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    public static Response ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Response ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Response ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static Response ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static Response ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Response ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static Response ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static Response ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static Response ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static Response ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private Response MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(Response prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<Response, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(Response cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private Response result;
      
      private Response PrepareBuilder() {
        if (resultIsReadOnly) {
          Response original = result;
          result = new Response();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override Response MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::messages.Response.Descriptor; }
      }
      
      public override Response DefaultInstanceForType {
        get { return global::messages.Response.DefaultInstance; }
      }
      
      public override Response BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is Response) {
          return MergeFrom((Response) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(Response other) {
        if (other == global::messages.Response.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasTypeId) {
          TypeId = other.TypeId;
        }
        if (other.HasMessage) {
          Message = other.Message;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_responseFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _responseFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 8: {
              result.hasTypeId = input.ReadInt32(ref result.typeId_);
              break;
            }
            case 26: {
              result.hasMessage = input.ReadString(ref result.message_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasTypeId {
        get { return result.hasTypeId; }
      }
      public int TypeId {
        get { return result.TypeId; }
        set { SetTypeId(value); }
      }
      public Builder SetTypeId(int value) {
        PrepareBuilder();
        result.hasTypeId = true;
        result.typeId_ = value;
        return this;
      }
      public Builder ClearTypeId() {
        PrepareBuilder();
        result.hasTypeId = false;
        result.typeId_ = 0;
        return this;
      }
      
      public bool HasMessage {
        get { return result.hasMessage; }
      }
      public string Message {
        get { return result.Message; }
        set { SetMessage(value); }
      }
      public Builder SetMessage(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasMessage = true;
        result.message_ = value;
        return this;
      }
      public Builder ClearMessage() {
        PrepareBuilder();
        result.hasMessage = false;
        result.message_ = "";
        return this;
      }
    }
    static Response() {
      object.ReferenceEquals(global::messages.Messages.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
