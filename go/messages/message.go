package messages

import (
	"bytes"
	"encoding/binary"
	"fmt"
	"io"
)

const (
	MessageTypeText = 1
)

type Message struct {
	MessageType int
}

func WriteString(writer io.Writer, str string) {
	binary.Write(writer, binary.BigEndian, uint16(len(str)))
	writer.Write([]byte(str))
}
func ReadString(reader io.Reader) string {
	var length uint16
	fmt.Println(reader)
	binary.Read(reader, binary.BigEndian, &length)
	fmt.Println(length)
	var buf []byte = make([]byte, int(length))
	reader.Read(buf)
	return string(buf)
}

func (msg *Message) GetBytes() []byte {
	return make([]byte, 0)
}

type TextMessage struct {
	Message
	Text string
}

func (msg *TextMessage) GetBytes() []byte {
	var buffer *bytes.Buffer = new(bytes.Buffer)

	WriteString(buffer, msg.Text)
	//var str string = ReadString(bytes.NewReader(buffer.Bytes()))
	var str string = ReadString(buffer)

	fmt.Println(str)

	return buffer.Bytes()
}

func OnlyForTest() {
	fmt.Println("Hello!")
}
