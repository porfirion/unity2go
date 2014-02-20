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
	for {
		connection, err := listener.AcceptTCP()
		if err != nil {
			fmt.Println(err)
		}

		fmt.Println("Connected! (", connection.RemoteAddr(), ")")

		/*var buffer []byte
		shouldClose := false
		for !shouldClose {
			buffer = make([]byte, 1000)
			n, err := connection.Read(buffer)

			if err != nil {
				fmt.Println("Received: ", n)
				fmt.Println("Error: ", err)
				shouldClose = true
			} else {
				fmt.Println("Received: ", n)
			}
		}
		connection.Close()
		fmt.Println("Connection closed")*/
		go wait(connection)
	}

	for {
	}
}

func wait(connection net.Conn) {
	defer connection.Close()

	for {

		//reader := bufio.NewReader(connection)

		var num uint16
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
		//fmt.Println(buffer.Bytes())
		fmt.Println(buffer.String())
		/*var message string
		binary.Read(bytes.NewReader(buffer.Bytes()), binary.BigEndian, &message)
		fmt.Println(message)*/

		//мы получили длину соощения. вычитываем все оставшиеся байты в буфер

		/*
			var buffer []byte = make([]byte, 1000)
			n, err := connection.Read(buffer)

			fmt.Println(n, "bytes read")
			if err == nil {
				fmt.Println("Received: ", string(buffer[:n]))
			} else {
				fmt.Println("Error: ", err)
				connection.Close()
				break
			}

			n, err = connection.Write(buffer)
			if err == nil {
				fmt.Println(n, "bytes written")
				//fmt.Println("Written: ", string(buffer[:n]))
			} else {
				fmt.Println("Error: ", err)
				connection.Close()
				break
			}
		*/
	}

	fmt.Println("Waiting finished")
}
