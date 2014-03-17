package main

import (
	//"bufio"
	"bytes"
	"encoding/binary"
	"fmt"
	"net"
)

var (
	tcpProtocol string = "tcp4"
	tcpAddr            = &net.TCPAddr{IP: net.IPv4(127, 0, 0, 1), Port: 25001}
	listener    net.Listener
	connection  net.Conn
)

func main() {
	listener, err := net.ListenTCP(tcpProtocol, tcpAddr)

	if err != nil {
		fmt.Println("Error opening listener: ", err)
		return
	}

	fmt.Println("Listening..", tcpAddr)

	// main loop
	for {
		connection, err := listener.AcceptTCP()
		if err != nil {
			fmt.Println(err)
		}

		fmt.Println("Connected! (", connection.RemoteAddr(), ")")
		go wait(connection)
	}
}

func processMessage(buffer []byte) {
	fmt.Println(buffer)
	var reader *bytes.Reader = bytes.NewReader(buffer)
	var typeCode int32
	err := binary.Read(reader, binary.BigEndian, &typeCode)
	if err != nil {
		fmt.Println(err)
	} else {
		fmt.Println("Message type: ", typeCode)
	}

	switch typeCode {
	case 1:
		fmt.Print("TextMessage (")
		var num uint16
		err := binary.Read(reader, binary.BigEndian, &num)
		if err != nil {
			fmt.Println(err)
		} else {
			fmt.Println(num, " bytes)")
		}
		var textBytes []byte = make([]byte, num)
		binary.Read(reader, binary.BigEndian, textBytes)
		fmt.Println(string(textBytes))
	default:
	}
}

func wait(connection net.Conn) {
	defer connection.Close()

	for {

		//reader := bufio.NewReader(connection)

		var num uint32
		//err := binary.Read(reader, binary.BigEndian, &num)
		err := binary.Read(connection, binary.BigEndian, &num)
		if err != nil {
			fmt.Println("Error: ", err)
			break
		}

		fmt.Println("Read: ", num)

		var required int = int(num)
		var buffer bytes.Buffer
		for required > 0 {
			var chunk []byte = make([]byte, required)
			n, err := connection.Read(chunk)
			if err != nil {
				fmt.Println("Error: ", err)
				break
			}
			required -= n
			buffer.Write(chunk[:n])
		}
		fmt.Println("Message length: ", buffer.Len())
		processMessage(buffer.Bytes())
	}

	fmt.Println("Waiting finished")
}
