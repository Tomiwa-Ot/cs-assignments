from PyQt5.QtWidgets import QFrame, QHBoxLayout
from PyQt5.QtWebEngineWidgets import QWebEngineView
from PyQt5.QtCore import QThread, pyqtSignal
from view.resources import Resources

class Map(QFrame):

    def __init__(self, tracker, loc, start_button) -> None:
        super().__init__()
        self.tracker = tracker
        self.loc = loc
        self.start_button = start_button
        self.setFrameShape(QFrame.StyledPanel)

        self.web_view = QWebEngineView()
        with open(Resources.unilag_map, 'r') as fh:
            self.web_view.setHtml(fh.read())
        self.web_view.loadFinished.connect(self.loadFinished)

        hbox = QHBoxLayout()
        hbox.addWidget(self.web_view)

        self.setLayout(hbox)

    def loadFinished(self, ok):
        if ok:
            self.start_button.setEnabled(True)

    def move(self, id, x, y):
        self.tracker.set_location(id, x, y)
        self.worker = Worker(id)
        self.worker.signal.connect(self.loc.update)
        self.worker.start()
        self.worker.wait()
        self.web_view.page().runJavaScript(f"move({id}, \"{x}\", \"{y}\")", self.update_location)
    
    def update_location(self, val):
        pass


class Worker(QThread):

    signal  = pyqtSignal(int)

    def __init__(self, id, parent=None) -> None:
        QThread.__init__(self, parent)
        self.id = id

    def run(self):
        self.signal.emit(self.id + 1)