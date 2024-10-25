Imports System.IO
Imports System.Linq

Public Class FileProcessor
    ' Winning Eleven 2002 Stats
    Public Property Feet As Integer
    Public Property Boots As Integer
    Public Property Aggression As Integer
    Public Property Curve As Integer
    Public Property Jump As Integer
    Public Property Head As Integer
    Public Property Technique As Integer
    Public Property PassAcc As Integer
    Public Property ShotAcc As Integer
    Public Property ShotPwr As Integer
    Public Property Defense As Integer
    Public Property Offense As Integer
    Public Property Acceleration As Integer
    Public Property Dribble As Integer
    Public Property Speed As Integer
    Public Property Stamina As Integer
    Public Property BodyBalance As Integer
    Public Property Response As Integer
    Public Property Age As Integer
    Public Property Body As Integer
    Public Property SkinColor As Integer
    Public Property FeetOutside As Integer
    Public Property Height As Integer
    Public Property HairColorFace As Integer
    Public Property HairFace As Integer
    Public Property HairColor As Integer
    Public Property Hair As Integer
    Public Property Position As Integer
    Public Property IDK As Integer

    ' Método para leer y procesar bytes desde un archivo
    Public Sub ReadToFile(filePath As String, offset As Long)
        ' Leer 12 bytes desde el archivo en la posición especificada
        Dim bytes(11) As Byte
        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
            fs.Position = offset
            fs.Read(bytes, 0, 12)
        End Using

        ' Invertir los bytes
        Dim invertedBytes(11) As Byte
        For i As Integer = 0 To bytes.Length - 1
            invertedBytes(i) = bytes(bytes.Length - 1 - i)
        Next

        ' Convertir los bytes a una cadena binaria
        Dim binString As String = String.Join("", invertedBytes.Select(Function(b) Convert.ToString(b, 2).PadLeft(8, "0"c)))

        ' Convertir binario a decimal (función auxiliar)
        Feet = BinToDec(binString.Substring(0, 2)) ' FEET (01)
        Boots = BinToDec(binString.Substring(2, 3)) ' BOOTS (110)
        Aggression = BinToDec(binString.Substring(5, 3)) ' AGRESSION (110)
        Curve = BinToDec(binString.Substring(8, 3)) ' CURVE (111)
        Jump = BinToDec(binString.Substring(11, 3)) ' JUMP (111)
        Head = BinToDec(binString.Substring(14, 3)) ' HEAD (111)
        Technique = BinToDec(binString.Substring(17, 3)) ' TECHNIQUE (111)
        PassAcc = BinToDec(binString.Substring(20, 3)) ' PASS (111)
        ShotAcc = BinToDec(binString.Substring(23, 3)) ' SHOT ACCURACY (111)
        ShotPwr = BinToDec(binString.Substring(26, 3)) ' SHOT POWER (111)
        Defense = BinToDec(binString.Substring(29, 3)) ' DEFENSE (111)
        Offense = BinToDec(binString.Substring(32, 3)) ' OFFENSE (111)
        Acceleration = BinToDec(binString.Substring(35, 3)) ' ACCELERATION (111)
        Speed = BinToDec(binString.Substring(38, 3)) ' SPEED (111)
        Dribble = BinToDec(binString.Substring(41, 3)) '  DRIBBLE (111)
        Stamina = BinToDec(binString.Substring(44, 3)) ' STAMINA (111)
        BodyBalance = BinToDec(binString.Substring(47, 3)) ' BODY BALANCE (011)
        Response = BinToDec(binString.Substring(50, 4)) ' RESPONSE (0111)
        Age = BinToDec(binString.Substring(54, 5)) ' AGE (11111)
        Body = BinToDec(binString.Substring(59, 3)) ' BODY (111)
        SkinColor = BinToDec(binString.Substring(62, 2)) ' SKIN COLOR (11)
        FeetOutside = BinToDec(binString.Substring(64, 1)) ' FEET OUTSIDE (1)
        IDK = BinToDec(binString.Substring(65, 5)) ' IDK (00000)
        Height = BinToDec(binString.Substring(70, 6)) ' HEIGHT (111111)
        HairColorFace = BinToDec(binString.Substring(76, 3)) ' HAIR COLOR FACE (110)
        HairFace = BinToDec(binString.Substring(79, 4)) ' HAIR FACE (0110)
        HairColor = BinToDec(binString.Substring(83, 4)) ' HAIR COLOR (0111)
        Hair = BinToDec(binString.Substring(87, 5)) ' HAIR (11111)
        Position = BinToDec(binString.Substring(92, 4)) ' POSITION (0111)
    End Sub

    ' Método para escribir los datos procesados en el archivo
    Public Sub WriteToFile(filePath As String, offset As Long)
        ' Concatenar todos los valores binarios en una sola cadena
        Dim binString As String = ""
        binString &= DecToBin(Feet, 2)
        binString &= DecToBin(Boots, 3)
        binString &= DecToBin(Aggression, 3)
        binString &= DecToBin(Curve, 3)
        binString &= DecToBin(Jump, 3)
        binString &= DecToBin(Head, 3)
        binString &= DecToBin(Technique, 3)
        binString &= DecToBin(PassAcc, 3)
        binString &= DecToBin(ShotAcc, 3)
        binString &= DecToBin(ShotPwr, 3)
        binString &= DecToBin(Defense, 3)
        binString &= DecToBin(Offense, 3)
        binString &= DecToBin(Acceleration, 3)
        binString &= DecToBin(Dribble, 3)
        binString &= DecToBin(Speed, 3)
        binString &= DecToBin(Stamina, 3)
        binString &= DecToBin(BodyBalance, 3)
        binString &= DecToBin(Response, 4)
        binString &= DecToBin(Age, 5)
        binString &= DecToBin(Body, 3)
        binString &= DecToBin(SkinColor, 2)
        binString &= DecToBin(FeetOutside, 1)
        binString &= DecToBin(IDK, 5)
        binString &= DecToBin(Height, 6)
        binString &= DecToBin(HairColorFace, 3)
        binString &= DecToBin(HairFace, 4)
        binString &= DecToBin(HairColor, 4)
        binString &= DecToBin(Hair, 5)
        binString &= DecToBin(Position, 4)

        ' Dividir la cadena binaria en bloques de 8 bits para convertirlos a bytes
        Dim newBytes(11) As Byte
        For i As Integer = 0 To newBytes.Length - 1
            newBytes(i) = Convert.ToByte(binString.Substring(i * 8, 8), 2)
        Next

        ' Invertir los bytes antes de escribir
        Dim invertedBytes(11) As Byte
        For i As Integer = 0 To newBytes.Length - 1
            invertedBytes(i) = newBytes(newBytes.Length - 1 - i)
        Next

        ' Escribir los bytes modificados en el archivo
        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Write)
            fs.Position = offset
            fs.Write(invertedBytes, 0, 12)
        End Using
    End Sub

    ' Convertir binario a decimal
    Private Function BinToDec(bin As String) As Integer
        Return Convert.ToInt32(bin, 2)
    End Function

    ' Convertir decimal a binario
    Private Function DecToBin(value As Integer, bits As Integer) As String
        Return Convert.ToString(value, 2).PadLeft(bits, "0"c)
    End Function
End Class
