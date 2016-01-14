import QtQuick 2.2
import QtQuick.Layouts 1.1
import QtQuick.Dialogs 1.1
import QtQuick.Controls 1.2

ApplicationWindow {
    visible: true
    title: "Qt Quick Controls Gallery"

    width: 640
    height: 480

    TextInput {
        id: textInput1
        x: 141
        y: 37
        width: 80
        height: 20
        text: qsTr("Text Input")
        font.pixelSize: 12
    }

    Button {
        id: button1
        x: 141
        y: 63
        text: qsTr("Button")
    }

    CheckBox {
        id: checkBox1
        x: 145
        y: 92
        text: qsTr("Check Box")
    }

    ComboBox {
        id: comboBox1
        x: 141
        y: 120
    }

    ProgressBar {
        id: progressBar1
        x: 141
        y: 154
    }


}

