Imports Microsoft.VisualBasic

Public Class DateValidator
    Shared _rtnMessage As String

    Public Shared Function ValidateDate(ByVal sentDate As String) As String

        'Validate date
        Dim sentDate_ As String() = Split(sentDate, "/")
        If sentDate_.Count <> 3 Then
            '_rtnMessage = "Missing or Invalid Expecting full date in dd/mm/yyyy format ..."
            _rtnMessage = "Javascript:alert('Missing or Invalid Expecting full date in dd/mm/yyyy format ...')"
            Return _rtnMessage
            'Exit Function
        End If

        Dim strMyDay = sentDate_(0)
        Dim strMyMth = sentDate_(1)
        Dim strMyYear = sentDate_(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)
        If Val(strMyYear) < 1999 Then
            _rtnMessage = "Javascript:alert('Error. Proposal year date is less than 1999 ...')"
            Return _rtnMessage
        End If

        Dim strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        _rtnMessage = Trim(strMyDte)


        Return _rtnMessage
    End Function

End Class
