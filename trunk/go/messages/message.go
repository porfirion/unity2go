package messages

import (
	"bytes"
	"encoding/binary"
	"fmt"
	"io"
)

const (
	MessageTypeBase  = 0
	MessageTypeText  = 1
	MessageTypeLogin = 2
)

type Message interface {
	SetType(MessageType uint32)
	GetType() uint32
	GetBytes() []byte
	FromBytes(reader io.Reader)
}

type BaseMessage struct {
	MessageType uint32
}

func WriteString(writer io.Writer, str string) {
	binary.Write(writer, binary.BigEndian, uint16(len(str)))
	writer.Write([]byte(str))
}
func ReadString(reader io.Reader) string {
	var length uint16
	//fmt.Println(reader)
	binary.Read(reader, binary.BigEndian, &length)
	//fmt.Println(length)
	var buf []byte = make([]byte, int(length))
	reader.Read(buf)
	return string(buf)
}

func (msg *BaseMessage) GetBytes() []byte {
	return make([]byte, 0)
}
func (msg *BaseMessage) SetType(MessageType uint32) {
	msg.MessageType = MessageType
}
func (msg *BaseMessage) GetType() uint32 {
	return msg.MessageType
}

type TextMessage struct {
	BaseMessage
	Text string
}

func (msg *TextMessage) GetBytes() []byte {
	var buffer *bytes.Buffer = new(bytes.Buffer)

	binary.Write(buffer, binary.BigEndian, uint32(msg.MessageType))

	WriteString(buffer, msg.Text)
	//var str string = ReadString(bytes.NewReader(buffer.Bytes()))

	return buffer.Bytes()
}
func (msg *TextMessage) FromBytes(reader io.Reader) {
	msg.Text = ReadString(reader)
}

func NewMessage(messageType uint32) Message {
	var message Message
	switch messageType {
	case MessageTypeText:
		message = new(TextMessage)
	case MessageTypeLogin:
		message = &LoginMessage{}
	default:
		panic("Unknown message type")
	}

	message.SetType(messageType)

	return message
}

func OnlyForTest() {
	fmt.Println("Hello!")
}

type LoginMessage struct {
	BaseMessage
	UserId   uint32
	UserName string
}

func (msg *LoginMessage) GetBytes() []byte {
	var buffer *bytes.Buffer

	binary.Write(buffer, binary.BigEndian, uint32(msg.UserId))
	WriteString(buffer, msg.UserName)

	return buffer.Bytes()
}
func (msg *LoginMessage) FromBytes(reader io.Reader) {
	binary.Read(reader, binary.BigEndian, msg.UserId)
	msg.UserName = ReadString(reader)
}

func FromBytes(buffer []byte) Message {
	reader := bytes.NewReader(buffer)
	var messageType uint32
	binary.Read(reader, binary.BigEndian, &messageType)
	fmt.Println("Message type: ", messageType)

	message := NewMessage(messageType)
	switch message.FromBytes(reader); message.(type) {
	case *TextMessage:
		fmt.Println("it is text message!")
	case *LoginMessage:
		fmt.Println("it is login message!")
	default:
		fmt.Println("it is unknown message!")
	}

	fmt.Println(message)

	return message
}
