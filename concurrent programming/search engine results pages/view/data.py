from PyQt5.QtCore import Qt, QThread, pyqtSignal
from PyQt5.QtWidgets import QFrame, QHBoxLayout, QTabWidget, QTextEdit, QLabel

class Data(QFrame):

    def __init__(self) -> None:
        super().__init__()
        self.setFrameShape(QFrame.StyledPanel)

        self.tab = [QTextEdit(), QTextEdit(), QTextEdit(), QTextEdit(), QTextEdit()]
        self.tabs = QTabWidget()
        for i, tab in enumerate(self.tab):
            self.tabs.addTab(tab, str(i + 1))

        self.text_view = QLabel('No summary available')
        # self.text_view.setReadOnly(True)
        self.text_view.setAlignment(Qt.AlignCenter)
        self.hbox = QHBoxLayout()
        self.hbox.addWidget(self.tabs)

        self.setLayout(self.hbox)

    def show_summary(self, data):
        for i, key in enumerate(data):
            self.tabs.setTabText(i, key)
            self.tab[i].setDocumentTitle(key)
            self.tab[i].setText(data[key])
            self.tab[i].setReadOnly(True)

    def update(self, data):
        self.worker = Worker(data)
        self.worker.signal.connect(self.show_summary)
        self.worker.start()
        self.worker.wait()

class Worker(QThread):

    signal = pyqtSignal(dict)

    def __init__(self, data, parent=None) -> None:
        QThread.__init__(self, parent)
        self.data = data

    def run(self):
        self.signal.emit(self.data)