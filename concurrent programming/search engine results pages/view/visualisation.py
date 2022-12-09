from PyQt5.QtCore import Qt, QThread, pyqtSignal
from PyQt5.QtWidgets import QFrame, QHBoxLayout
from PyQt5.QtChart import QChart, QBarSet, QChartView, QValueAxis, QBarSeries

class Visualisation(QFrame):

    def __init__(self) -> None:
        super().__init__()
        self.setFrameShape(QFrame.StyledPanel)

        self.sets = None

        self.series = QBarSeries()

        self.chart = QChart()
        self.chart.setTitle('Relevancy')
        self.chart.setAnimationOptions(QChart.SeriesAnimations)

        y_axis = QValueAxis()
        y_axis.setRange(0, 100)

        self.chart.addAxis(y_axis, Qt.AlignLeft)
        self.chart.legend().setVisible(True)
        self.chart.legend().setAlignment(Qt.AlignBottom)

        self.chart_view = QChartView(self.chart)

        hbox = QHBoxLayout()
        hbox.addWidget(self.chart_view)

        self.setLayout(hbox)

    
    def populate_graph(self, data):
        self.chart.removeSeries(self.series)
        self.series = QBarSeries()
        self.sets = [QBarSet(''), QBarSet(''), QBarSet(''), QBarSet(''), QBarSet('')]
        for i, key in enumerate(data):
            self.sets[i].setLabel(key)
            self.sets[i].append(data[key])
            self.series.append(self.sets[i])
        self.chart.addSeries(self.series)
        

    def update(self, data):
        self.worker = VisualWorker(data)
        self.worker.signal.connect(self.populate_graph)
        self.worker.start()
        # self.worker.wait()


class VisualWorker(QThread):

    signal = pyqtSignal(dict)

    def __init__(self, data, parent=None) -> None:
        QThread.__init__(self, parent)
        self.data = data

    def run(self):
        print(self.data)
        self.signal.emit(self.data)